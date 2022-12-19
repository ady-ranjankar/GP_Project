using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
using TMPro;

public class GenQuestions : MonoBehaviour
{
    // Start is called before the first frame update

    string answer;
    public TMP_Text screen;
    public TMP_Text screen1;
    public TMP_Text screen2;
    public TMP_Text screen3;
    public TMP_Text screen4;
    public string createQuest(int level)
    {
       
        string file;
        if(level == 1)
            file = "Questions1.csv";
        else if(level == 2)
            file = "Questions2.csv";
        else if(level == 3)
            file = "Questions3.csv";
        else if(level == 4)
            file = "Questions4.csv";
        else 
            file = "Questions5.csv";
        
        StreamReader sr = new StreamReader(file);
        List<List<string>> questions = new List<List<string>>();
        string text = "";
        for (int i = 1; i <= 4; i++) 
        {
            string line = sr.ReadLine();
            string[] item = line.Split(',');
            questions.Add(item.ToList());
            
           
        }
        
        sr.Close();
        System.Random r = new System.Random();
        int rInt = r.Next(0, questions.Count);

        
        screen.text = questions[rInt][0];
        screen1.text = "A. " + questions[rInt][1];
        screen2.text = "B. " + questions[rInt][2];
        screen3.text = "C. " + questions[rInt][3];
        screen4.text = "D. " + questions[rInt][4];

        answer = questions[rInt][5];
        return answer;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}



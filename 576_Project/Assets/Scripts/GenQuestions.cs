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
    public string createQuest()
    {
       
        StreamReader sr = new StreamReader("Questions.csv");
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

        
        for (int j = 0; j<5; j++)
            text = string.Concat(text, questions[rInt][j],"\n");
                
        screen.text = text;

        answer = questions[rInt][5];
        return answer;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}



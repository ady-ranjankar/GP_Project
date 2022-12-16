using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI option3;
    public TextMeshProUGUI option4;
    public TextMeshProUGUI messageText; //can be removed later
    public InputField inField;

    public string questionFrame;
    public string[] elementsNames;
    public string[] option1List;
    public string[] option2List;
    public string[] option3List;
    public string[] option4List;
    public string[] answers;
    public string[] correctAnswer;

    Dictionary<int, List<string>> options = new Dictionary<int, List<string>>();

    void Start()
    {
        var filename = "Questions/Questions.txt";
        var source = new StreamReader(Application.dataPath + "/" + filename);
        var fileContents = source.ReadToEnd();
        source.Close();
        elementsNames = fileContents.Split("\n"[0]);
        
        filename = "Questions/Option1.txt";
        source = new StreamReader(Application.dataPath + "/" + filename);
        fileContents = source.ReadToEnd();
        source.Close();
        option1List = fileContents.Split("\n"[0]);

        filename = "Questions/Option2.txt";
        source = new StreamReader(Application.dataPath + "/" + filename);
        fileContents = source.ReadToEnd();
        source.Close();
        option2List = fileContents.Split("\n"[0]);

        filename = "Questions/Option3.txt";
        source = new StreamReader(Application.dataPath + "/" + filename);
        fileContents = source.ReadToEnd();
        source.Close();
        option3List = fileContents.Split("\n"[0]);

        filename = "Questions/Option4.txt";
        source = new StreamReader(Application.dataPath + "/" + filename);
        fileContents = source.ReadToEnd();
        source.Close();
        option4List = fileContents.Split("\n"[0]);

        filename = "Questions/CorrectAnswers.txt";
        source = new StreamReader(Application.dataPath + "/" + filename);
        fileContents = source.ReadToEnd();
        source.Close();
        correctAnswer = fileContents.Split("\n"[0]);


    }

    public void generateQuestion()
    {
        //Debug.Log(elementsNames.Length);
        //Debug.Log(elementsNames[0]);
        int i = UnityEngine.Random.Range(0,elementsNames.Length);
        questionText.text = questionFrame + elementsNames[i];
        option1.text = questionFrame + option1List[i];
        option2.text = questionFrame + option2List[i];
        option3.text = questionFrame + option3List[i];
        option4.text = questionFrame + option4List[i];

        messageText.text = questionFrame + correctAnswer[i];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ViewScores : MonoBehaviour
{
    // Start is called before the first frame update
    public Text score_board;
    private GameObject plane;
    private GameObject scoresButton;
    private GameObject helpButton;
    private GameObject exitButton;


    [SerializeField]
    private StringSO nameSO;
    [SerializeField]
    private FloatSO scoreSO;

    private Text score_text;
    private float score; 
    private string score_file;
    private string[] players;
    private float[] scores;

    // set up the the hall of fame screen
    void Start(){
        gameObject.SetActive(false);
        plane = GameObject.Find("Panel");
        scoresButton = GameObject.Find("viewScores");
        helpButton = GameObject.Find("Help");
        exitButton = GameObject.Find("Exit");
        Debug.Log("Name: " + GameManager.instance.nickname.text);
        Debug.Log("Name FINAL:  " + nameSO.Value);
        score_file = "scores.txt";
        scores = new float[5]; 
        players = new string[5];
        score = scoreSO.Value;

        writeScores();

    }
    private void writeScores() {
        
        if (!File.Exists(score_file)) {
            TextWriter tw = new StreamWriter(score_file, true);
            for (int k = 0; k < 5; k++) {
                tw.WriteLine("Empty, 0");
            }
            tw.Close();
        }
        StreamReader sr = new StreamReader(score_file);
        for (int i = 0; i < 5; i++) 
        {
            string line = sr.ReadLine();
            string[] elements = line.Split(',');
            float player_score = int.Parse(elements[1].Trim());

            scores[i] = player_score;
            players[i] = elements[0];
            
        }
        sr.Close();

        if (score > scores[4]) {
            scores[4] = score;
            players[4] = nameSO.Value;
        }

        for (int i = 0; i < 5; i++) {
            for (int j = i + 1; j < 5; j++) {
                if(scores[i] < scores[j])
                {
                    float temp_score = scores[i];
                    string temp_name = players[i];
                    scores[i] = scores[j];
                    players[i] = players[j];
                    scores[j] = temp_score;
                    players[j] = temp_name;
                }
            }
        }

        StreamWriter writer = new StreamWriter(score_file);
        for (int i = 0; i < 5; i++)
        {
            string to_write = string.Format("{0}, {1}", players[i], scores[i]);
            writer.WriteLine(to_write);
        }
        writer.Close();
    }


    public void Setup()
    {
        gameObject.SetActive(true);
        plane.SetActive(false);
        scoresButton.SetActive(false);
        helpButton.SetActive(false);
        exitButton.SetActive(false);
        Debug.Log("Scores" + scoreSO.Value);

        StreamReader sr = new StreamReader("scores.txt");
        string text = "Name : Score\n\n ";
        for (int i = 1; i <= 5; i++) 
        {
            string line = sr.ReadLine();
            string[] item = line.Split(',');
            string entry = string.Format("{0} : {1}\n\n", item[0], item[1]);
            text = string.Concat(text, entry);
        }
        
        sr.Close();
        score_board.text = text;
    }

    // public void HelpButton()
    // {
    //     SceneManager.LoadScene("CannonGame");
    // }
}

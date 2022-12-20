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
    private FloatSO scoreSO;

    // set up the the hall of fame screen
    void Start(){
        gameObject.SetActive(false);
        plane = GameObject.Find("Panel");
        scoresButton = GameObject.Find("viewScores");
        helpButton = GameObject.Find("Help");
        exitButton = GameObject.Find("Exit");

    }
    public void Setup()
    {
        gameObject.SetActive(true);
        plane.SetActive(false);
        scoresButton.SetActive(false);
        helpButton.SetActive(false);
        exitButton.SetActive(false);
        Debug.Log("Level 1 scores" + scoreSO.Value);

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

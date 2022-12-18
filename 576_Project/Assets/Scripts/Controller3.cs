using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Controller3 : MonoBehaviour
{

    private float timeRemaining = 3.0f;
    public bool timerIsRunning = false;
    public TMP_Text timer_text ;
    private float time_left;
    private int level;
    public CreateEnvironment create;

    public Opponent opponent;
    public GenQuestions questiongen;



    public GameObject claire;

    public Behaviour NextLevel;
    public Behaviour NextLevel_text;

    private string diff_level;
    public TMP_Text diff_level_text;

    int opp_answer;
    int act_answer;
    int answer;
    int g_over = 0;
    Color fin_color;
    // Start is called before the first frame update
    void Start()
    {
        time_left = 30.0f;
        level = 3;
        create.begin(level);
        opp_answer = get_opponent_answer();
        act_answer = get_actual_answer();
        if(act_answer == 1)
            fin_color = Color.red;

        if(act_answer == 2)
            fin_color = Color.blue;

        if(act_answer == 3)
            fin_color = Color.yellow;

        if(act_answer == 4)
            fin_color = Color.green;
        opponent.get_route(act_answer);
        opponent.isRoute = true;
        timerIsRunning = true;
        NextLevel.enabled = false;
        NextLevel_text.enabled = false;
        //claire.SetActive(false);
        StartCoroutine("Timer");
        diff_level = GameManager.instance.difficulty_level;
        diff_level_text.text = "Level: " + diff_level;
    }

    int get_opponent_answer(){
        //Probability to get answer
        return 1;
    }

    int get_actual_answer(){
        //Probability to get answer
        return Convert.ToInt16(questiongen.createQuest(level));
    }

    public void Scene_Change()
    {
        Debug.Log("Scene change");
        SceneManager.LoadScene("Scene4");
    }

    public void onPauseButton(){
        SceneManager.LoadScene("MainMenu2");
    }

    public void onExitButton(){
        SceneManager.LoadScene("ExitMenu");
    }

    private IEnumerator Timer() {
        while (time_left > 0.0f) { // decrement time left
            time_left -= 0.01f;
            if (create.timeRemaining > 0.0f) {
                timer_text.text = "Time Left : " + time_left.ToString("F2");    
            }
            yield return new WaitForSeconds(0.01f);
        }
        timer_text.gameObject.SetActive(false);
        StopCoroutine("Timer");    
    }


    // Update is called once per frame
        void Update()
    {
    if(claire.transform.position.y < -1.85)
            g_over = 1;

        if(claire.transform.position.y < -13.85)    
            SceneManager.LoadScene("ExitMenu");

        if (timerIsRunning)
        {
            if (create.timeRemaining > 0)
            {
                create.timeRemaining -= Time.deltaTime;
            }
            else if(g_over == 0)
            {
                timerIsRunning = false;
                NextLevel.enabled = true;
                NextLevel_text.enabled = true;
                String n;
                GameObject tile;
                int i;
                int j;
                for (i = 0; i < 10; i++)
                {
                    for(j = 0; j < 10; j++)
                    {
                        
                        n = "TILE" + (i * 10 + j).ToString();
                        tile = GameObject.Find(n);
                        try
                        {
                            if(tile.GetComponent<Renderer>().material.color != fin_color)
                                UnityEngine.Object.Destroy(tile);
                        }    

                        catch (Exception error)
                        {
                            Debug.Log("Tile not found");
                                
                        }
                        
                    }
                }

                /*
                level ++;
                //code to choose question and answer
                answer = 1;
                opponent.isRoute = false;
                GameObject opp = GameObject.Find("opp");
                UnityEngine.Object.Destroy(opp);
                claire.SetActive(false);
                create.begin(level);
                opponent.get_route(1);
                opponent.isRoute = true;

                Debug.Log("Time has run out!");
                
                foreach (Tile x in create.opponent_to_A) 
                {
                    Debug.Log(x.row);
                    Debug.Log(x.col);
                }


                if (level == 5)
                    timerIsRunning = false;
                
                //create.destroy();
                
                timeRemaining = 30.0f;
                */
            }
        }
    }

}


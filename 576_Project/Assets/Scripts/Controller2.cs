using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2 : MonoBehaviour
{

    private float timeRemaining = 15.0f;
    public bool timerIsRunning = false;

    private int level;
    public CreateEnvironment create;

    public Opponent opponent;

    public GameObject claire;

    public Behaviour NextLevel;
    public Behaviour NextLevel_text;

    int opp_answer;
    int act_answer;

    int answer;
    Color fin_color;
    // Start is called before the first frame update
    void Start()
    {
        level = 2;
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
        opponent.get_route(1);
        opponent.isRoute = true;
        timerIsRunning = true;
        NextLevel.enabled = false;
        NextLevel_text.enabled = false;
        GameObject scrolling_image = GameObject.Find("ScrollingImage");
        scrolling_image.SetActive(false);
        GameObject background_panel = GameObject.Find("BackgroundPanel");
        background_panel.SetActive(false);
        claire.SetActive(false);

        //claire.SetActive(false);
    }

    int get_opponent_answer(){
        //Probability to get answer
        return 1;
    }

    int get_actual_answer(){
        //Probability to get answer
        return 1;
    }

    public void Scene_Change()
    {
        
        SceneManager.LoadScene("Scene1");

    }

    public void onPauseButton(){
        SceneManager.LoadScene("MainMenu2");
    }

    public void onExitButton(){
        SceneManager.LoadScene("ExitMenu");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeRemaining);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timerIsRunning = false;

                // GameObject scrolling_image = GameObject.Find("ScrollingImage");
                // scrolling_image.SetActive(true);
                // GameObject background_panel = GameObject.Find("BackgroundPanel");
                // background_panel.SetActive(true);
                // claire.SetActive(false);

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


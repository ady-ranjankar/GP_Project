using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2 : MonoBehaviour
{

    private float timeRemaining = 3.0f;
    public bool timerIsRunning = false;

    private int level;
    public CreateEnvironment create;

    public Opponent opponent;

    public GameObject claire;

    int answer;
    // Start is called before the first frame update
    void Start()
    {
        level = 2;
        create.begin(level);
        opponent.get_route(1);
        opponent.isRoute = true;
        timerIsRunning = true;
        //claire.SetActive(false);
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
                 SceneManager.LoadScene("Scene3");
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


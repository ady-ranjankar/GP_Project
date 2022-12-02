using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private float timeRemaining = 10.0f;
    public bool timerIsRunning = false;

    private int level;
    public CreateEnvironment create;

    public Opponent opponent;

    int answer;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        create.begin(level);
        opponent.get_route(1);
        opponent.isRoute = true;
        timerIsRunning = true;
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
                
                level ++;
                //code to choose question and answer
                answer = 1;
                opponent.isRoute = false;
                GameObject opp = GameObject.Find("opp");
                UnityEngine.Object.Destroy(opp);
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
                
                timeRemaining = 10.0f;
            }
        }
    }

}


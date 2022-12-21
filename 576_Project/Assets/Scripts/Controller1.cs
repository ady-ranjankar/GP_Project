using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Controller1 : MonoBehaviour
{

    public Vector3 movement_direction;
    public bool timerIsRunning = false;
    public TMP_Text timer_text ;
    private float time_left;
    private int level;
    public CreateEnvironment create;
    public Claire clr;

    public TMP_Text msg;
    private bool update_score_called ;

    public Opponent opponent;

    public GenQuestions questiongen;

    // private string diff_level;
    // public TMP_Text diff_level_text;

    public GameObject claire;

    public Behaviour NextLevel;
    public Behaviour NextLevel_text;

    private float max_level_score;
    private float current_level_score;
    private GameObject resume;
    private GameObject pause;

    private bool player_won;


    private AudioClip failed_audio;
    private AudioSource audio_source;

    int opp_answer;
    int act_answer;
    int g_over = 0;
    int answer;
    int down_move = 0;
    Color fin_color;

    [SerializeField]
    private FloatSO scoreSO;

   
   private void UpdateScore()
   {
       scoreSO.Value += level*10;
       Debug.Log("Update Score " + scoreSO.Value);
   }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        
        level = 1;
        max_level_score = level*10.0f;
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
        // diff_level = GameManager.instance.difficulty_level;
        // diff_level_text.text = "Level: " + diff_level;
        player_won = false;



        pause = GameObject.Find("PauseButton");
        resume = GameObject.Find("ResumeButton");
        resume.SetActive(false);

        audio_source = gameObject.GetComponent<AudioSource>();
        failed_audio = Resources.Load<AudioClip>("Sounds/failed_music");
        Debug.Log(failed_audio != null);

        msg.enabled = false;
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
        SceneManager.LoadScene("Scene2");
    }

    public void onResumeButton(){
        StartCoroutine("Timer");  
        Time.timeScale = 1;
        resume.SetActive(false);
        pause.SetActive(true);
    }
    public void onPauseButton(){
        StopCoroutine("Timer");     
        // SceneManager.LoadScene("MainMenu2");
        Time.timeScale = 0;
        pause.SetActive(false);
        resume.SetActive(true);

    }


    public void onExitButton(){
        SceneManager.LoadScene("ExitMenu");
    }

    private IEnumerator Timer() {
        while (create.timeRemaining > 0.0f) { // decrement time left
            create.timeRemaining -= 0.01f;
            if (create.timeRemaining > 0.0f) {
                timer_text.text = "Time Left : " + create.timeRemaining.ToString("F2");    
            }
            yield return new WaitForSeconds(0.01f);
        }
        timer_text.gameObject.SetActive(false);
        StopCoroutine("Timer");    
        update_score_called = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (player_won && !update_score_called )
        {
            UpdateScore();
            // player_won = false;
            update_score_called = true;
        }
        // //Debug.Log(timeRemaining);

        if(claire.transform.position.y < -2.00)
            g_over = 1; // Claire Lost, Game is Over

        if(claire.transform.position.y < -13.85)
            SceneManager.LoadScene("ExitMenu");

        

        if (timerIsRunning)
        {
            if (create.timeRemaining > 0)
            {
                create.timeRemaining -= Time.deltaTime;   
            }
            else if(g_over ==0) //
            {
                // Debug.Log("Claire Lost!");
                timerIsRunning = false;

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
                

                
            }
        }
        else{
            RaycastHit hit;
            if(Physics.Raycast(claire.transform.position, claire.transform.TransformDirection (Vector3.down), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.name.Contains("TILE"))
                {
                    Debug.Log("found tilesssssssssss");
                    Debug.Log("Claire Passed"); // Claire passed and time over/ game over
                    if (!player_won && !update_score_called){
                        player_won = true;
                    }
                    msg.enabled = true;
                    msg.text =  "Congratulations! You Passed this level";    
                    NextLevel.enabled = true;
                    NextLevel_text.enabled = true;
                    Time.timeScale = 0;
                }
                else
                {
                    clr.down_move = 1;
                    msg.enabled = true;
                    msg.text =  "Ohh no! Level Failed!";  
                    audio_source.PlayOneShot(failed_audio);

                }
            }
        }
        
        
    }

}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Text timer_text;
    // public GameObject game_start_scene;
    // public GameEnd game_end_scene;
    // // public MainMenu2 game_resume_scene;
    private float time_left; // individual scene
    private bool is_game_running;
    private Text score_text;  // individual scene
    public TMP_InputField nickname;
    private int score; 
    private string player_nickname;
    private string score_file;
    private string[] players;
    private int[] scores;


    [SerializeField]
    private StringSO nameSO;
    
    public static GameManager instance;
    // public string difficulty_level;
    public float player_score; 
    // Start is called before the first frame update

    public void Awake(){
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        player_score = 0.0f;
        // timer_text.gameObject.SetActive(false);
        GameObject.Find("GameStart").SetActive(true);
        // GameObject.Find("GameEnd").SetActive(false);
        // GameObject.Find("MainMenu2").SetActive(false);

        time_left = 30.0f;
        is_game_running = true;
        // timer_text.text = "Time  : " + time_left.ToString("F2"); 
        score_file = "scores.txt";
        scores = new int[5]; 
        players = new string[5];
        score = 0; 
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
            int player_score = int.Parse(elements[1].Trim());

            scores[i] = player_score;
            players[i] = elements[0];
            
        }
        sr.Close();

        if (score > scores[4]) {
            scores[4] = score;
            players[4] = player_nickname;
        }

        for (int i = 0; i < 5; i++) {
            for (int j = i + 1; j < 5; j++) {
                if(scores[i] < scores[j])
                {
                    int temp_score = scores[i];
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


    // private IEnumerator Timer() {
    //     while (time_left > 0.0f) { // decrement time left
    //         time_left -= 0.1f;
    //         if (time_left > 0.0f) {
    //             timer_text.text = "Time Left : " + time_left.ToString("F2");    
    //         }
    //         yield return new WaitForSeconds(0.1f);
    //     }
    //     is_game_running = false;
    //     score_text.gameObject.SetActive(false);
    //     timer_text.gameObject.SetActive(false);
    //     StopCoroutine("Timer"); 
    //     writeScores();
    //     // hall_of_fame.Setup();    
    // }

    // public string GetDifficultyLevel(){
    //     // Debug.Log(difficulty_level);
    //     return difficulty_level;
    // }

    // public void SetDifficultyLevel(int index){
    //     if(index == 0){
    //         difficulty_level = "EASY";
    //     }
    //     else if(index == 1){
    //         difficulty_level = "MEDIUM";
    //     }
    //     else{
    //         difficulty_level = "HARD";
    //     }
    //    Debug.Log("Level Selected: " + difficulty_level);

    // }

    public void SetPlayerNickname(){
       
        player_nickname = nickname.text;
        Debug.Log(player_nickname);
        nameSO.Value = player_nickname;
        GameObject.Find("GameStart").SetActive(false);
        SceneManager.LoadScene("Scene1");
        // score_text.gameObject.SetActive(true);
        // timer_text.gameObject.SetActive(true);
         

    }
    // Update is called once per frame
    void Update()
    {
        // if (is_game_running){
        //     score_text.text = "Score: " + score;
        // }
        
        
    }
}

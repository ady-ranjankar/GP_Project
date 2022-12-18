using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    private float startTime;
    private float levelTimer = 30;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        
    }

    // Update is called once per frame
    void Update()
    {
        float timeRemaining = Time.time - startTime;

        string remMinutes = ((int) timeRemaining / 60).ToString();
        float remSeconds = timeRemaining % 60;
        string remTimer = (levelTimer - remSeconds).ToString("f0");

        timerText.text = "00:" + remTimer ;
    }
}

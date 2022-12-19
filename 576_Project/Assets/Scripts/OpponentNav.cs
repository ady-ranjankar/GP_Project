using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentNav : MonoBehaviour
{

    public CreateEnvironment create;

    private List<Tile> route = new List<Tile>();
    public bool isRoute;
    float y = 0.0f;
    int i = 0;
    private CharacterController character_controller;
    private UnityEngine.AI.NavMeshAgent navM;
    public Animator animation_controller;
    private bool isWalking = false;
    Color fin_color;
    private Vector3 destination;
    // Start is called before the first frame update

    public void get_route(int answer)
    {
        Debug.Log("Route");
        if(answer == 1)
            fin_color = Color.red;

        if(answer == 2)
            fin_color = Color.blue;

        if(answer == 3)
            fin_color = Color.yellow;

        if(answer == 4)
            fin_color = Color.green;

        isRoute = true;
        int i;
        int j;
        String n;
        GameObject tile;
        int found = 0;
        for (i = 0; i < 10; i++)
        {
            for(j = 0; j < 10; j++)
            {
                
                n = "TILE" + (i * 10 + j).ToString();
                tile = GameObject.Find(n);
                try
                {
                    if(tile.GetComponent<Renderer>().material.color == fin_color)
                    {
                        destination = tile.transform.position;
                        destination.y = -1.91f;
                        found = 1;
                    }    
                }    

                catch (Exception error)
                {
                    Debug.Log("Tile not found");
                        
                }
                if (found == 1)
                    break;
                
            }
            if (found == 1)
                break;
        }

        GameObject opp = GameObject.Find("opp");

        navM = opp.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        
        Debug.Log("finish");
    }


    IEnumerator StartAnimation(){
        GameObject opp = GameObject.Find("opp");
       
        animation_controller = opp.GetComponent<Animator>();
        // Debug.Log("Inside Coroutine");
        isWalking = true;
        animation_controller.SetBool("isWalkingForward",isWalking);
        yield return new WaitForSeconds(3f);
    }

    IEnumerator EndAnimation(){

        GameObject opp = GameObject.Find("opp");
       
        animation_controller = opp.GetComponent<Animator>();
        // Debug.Log("Inside Coroutine");
        isWalking = false;
        animation_controller.SetBool("isWalkingForward",isWalking);
        yield return new WaitForSeconds(3f);

    }




    // Update is called once per frame
    void Update()
    {
        GameObject opp = GameObject.Find("opp");
        if (isRoute && opp.transform.position != destination)
        {
            navM.destination = destination;
        }
        else
        {
           StartCoroutine(EndAnimation());
        }
        
        
    }
}

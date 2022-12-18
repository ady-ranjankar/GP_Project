using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{

    public CreateEnvironment create;

    private List<Tile> route = new List<Tile>();
    public bool isRoute;
    float y = 0.0f;
    int i = 0;
    private CharacterController character_controller;
    public Animator animation_controller;
    private bool isWalking = false;

    // Start is called before the first frame update

    public void get_route(int answer)
    {
        Debug.Log("Route");
        if(answer == 1)
            route = create.opponent_to_A;

        else if(answer == 2)
            route = create.opponent_to_B;
    
        else if(answer == 3)
            route = create.opponent_to_C;

        else if(answer == 4)
            route = create.opponent_to_D;

        isRoute = true;
        i = 0;

        foreach (Tile x in route) 
                {
                    Debug.Log(x.row);
                    Debug.Log(x.col);
                }
        
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



    Vector3 get_direction()
    {
        
        Tile x = route[i];
        String name;
        //Debug.Log("Move");
        //Debug.Log(i);
        name = "TILE" + (x.row*create.size + x.col).ToString();
        GameObject tile = GameObject.Find(name);

        GameObject opp = GameObject.Find("opp");
        Debug.Log("Tile y location: " + tile.transform.position.y);
        Vector3 tile_pos = tile.transform.position;
        
        Vector3 cur_pos = opp.transform.position;
        y = cur_pos.y;
        Vector3 direction = tile_pos - cur_pos;
        direction.y = 0.0f;
        direction.Normalize();
        return direction;

        ///float angle_to_rotate = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        //opp.transform.eulerAngles = new Vector3(0.0f, angle_to_rotate_turret, 0.0f);
        //Vector3 current_turret_direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 1.1f, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));

    }

    // Update is called once per frame
    void Update()
    {
        String name;
        

        if(isRoute && i < route.Count)
        {
            GameObject opp = GameObject.Find("opp");
            character_controller = opp.GetComponent<CharacterController>();
            animation_controller = opp.GetComponent<Animator>();
            // animation_controller.SetBool("isWalkingForward",true);
            // Debug.Log("State here " + animation_controller.GetBool("isWalkingForward"));
            if(i >= route.Count)
            {
                    isRoute = false;
                    return;
            }
        
            Vector3 direction = get_direction();
            Tile x = route[i];
            name = "TILE" + (x.row*create.size + x.col).ToString();
            GameObject tile = GameObject.Find(name);

            Vector3 tile_pos = tile.transform.position;
            tile_pos.y = y;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            opp.transform.rotation = rotation;

            opp.transform.position +=  4.0f * direction * Time.deltaTime;
            

            /////////////////////
            Vector3  dist = tile_pos - opp.transform.position ;
            Vector3 moveDist = dist;
            moveDist.y = 0.0f;
            moveDist.Normalize();
            float distanceToTarget = moveDist.sqrMagnitude;
            Debug.Log("distanceToTarget" + distanceToTarget);
            if(distanceToTarget > 0.1f )
                {
                   isWalking = true;
                   StartCoroutine(StartAnimation());
                }

            // if(i < route.Count){
            //     StartCoroutine(StartAnimation());
            // }
            ///////////////////
            if(Vector3.Distance(opp.transform.position,tile_pos) < 0.1f)
            {
                i++;
            } 
            /*
            Tile x = route[i];
            GameObject opp = GameObject.Find("opp");
            name = "TILE" + (x.row*create.size + x.col).ToString();
            GameObject tile = GameObject.Find(name);

            opp.transform.position = tile.transform.position;
            i++;
            if(i==route.Count)
                isRoute = false;
            */
        }
        else
        {
           StartCoroutine(EndAnimation());
        }
        
        
    }
}

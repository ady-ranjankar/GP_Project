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

    Vector3 get_direction()
    {
        
        Tile x = route[i];
        String name;
        //Debug.Log("Move");
        //Debug.Log(i);
        name = "TILE" + (x.row*create.size + x.col).ToString();
        GameObject tile = GameObject.Find(name);

        GameObject opp = GameObject.Find("opp");

        Vector3 tile_pos = tile.transform.position;
        
        Vector3 cur_pos = opp.transform.position;
        y = cur_pos.y;
        Vector3 direction = tile_pos - cur_pos;
        direction.y = y;
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
            opp.transform.position +=  4.0f * direction * Time.deltaTime;
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
        
        
    }
}

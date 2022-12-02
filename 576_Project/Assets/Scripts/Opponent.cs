using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{

    public CreateEnvironment create;

    private List<Tile> route = new List<Tile>();
    public bool isRoute = false;
    // Start is called before the first frame update
    public void get_route(int answer)
    {
        if(answer == 1)
            route = create.opponent_to_A;

        else if(answer == 2)
            route = create.opponent_to_B;
    
        else if(answer == 3)
            route = create.opponent_to_C;

        else if(answer == 4)
            route = create.opponent_to_D;

        
    }

    // Update is called once per frame
    void Update()
    {

        if(isRoute){

            //Code to move opponent
        }
        
    }
}

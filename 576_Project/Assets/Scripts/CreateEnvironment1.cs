using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CreateEnvironment1 : MonoBehaviour
{
    public float width;
    public int size;
    public GameObject myPrefab;
    public GameObject opp_prefab;
    
    

    public GameObject player;

    public float timeRemaining = 30.0f;

    //public Type claire_script;

    private List<int[]> pos_A;
    private List<int[]> pos_B;
    private List<int[]> pos_C;
    private List<int[]> pos_D;

    public List<Tile> player_to_A = new List<Tile>();
    public List<Tile> player_to_B = new List<Tile>();
    public List<Tile> player_to_C = new List<Tile>();
    public List<Tile> player_to_D = new List<Tile>();

    public List<Tile> opponent_to_A = new List<Tile>();
    public List<Tile> opponent_to_B = new List<Tile>();
    public List<Tile> opponent_to_C = new List<Tile>();
    public List<Tile> opponent_to_D = new List<Tile>();




    private List<TileType>[,] grid;

    private int[] player_start;
    private int[] opponent_start;

    private int function_calls = 0; 

    private void Start(){
        timeRemaining = 30.0f;
    }

    private void Update() 
    {

        try 
        {
            float speed = 20.0f;

            String n1 = "clock1";
            GameObject clock1 = GameObject.Find(n1);
            clock1.transform.Rotate (speed*Time.deltaTime,speed*Time.deltaTime,speed*Time.deltaTime);

            String n2 = "clock2";
            GameObject clock2 = GameObject.Find(n2);
            clock2.transform.Rotate (speed*Time.deltaTime,speed*Time.deltaTime,speed*Time.deltaTime);
        }

/* if there are any problems/error the catch will activate what
is written inside it */

        catch (Exception error)
        {
           Debug.Log("Clock not found");
                
        }
    }

    private void Shuffle<T>(ref List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    //1 - 100    30
    //2 - 80     23
    //3 - 60     18
    //4 - 30     9
    //5 - 10     3
    // Start is called before the first frame update
    public void destroy()
    {
        int i;
        int j;
        GameObject tile;
        String n;
        Debug.Log("Destroy");
        for (i = 0; i < size; i++)
            for(j = 0; j < size; j++)
            {
                
                n = "TILE" + (i * size + j).ToString();
                tile = GameObject.Find(n);
                //Debug.Log(tile.GetType().ToString());
                
                UnityEngine.Object.Destroy(tile);
                //Debug.Log("Destroying tile at "+ i + ", " + j);

                
            }
        
        String n1 = "clock1";
        GameObject clock1 = GameObject.Find(n1);
        UnityEngine.Object.Destroy(clock1);

        String n2 = "clock2";
        GameObject clock2 = GameObject.Find(n2);
        UnityEngine.Object.Destroy(clock2);
                

    }




    public void begin(int level)
    {
        size = 10;
        width = 5.0f;
        Debug.Log(level);
        if (level > 1)
        {
            destroy();
            
        }

        Debug.Log("hi");
        List<int[]> unassigned = new List<int[]>();
        List<int[]> unassigned_new = new List<int[]>();
        grid = new List<TileType>[size, size];
        
        

        bool success = false;
        int i;
        int j;
        String n;

        GameObject tile;

        pos_A = new List<int[]>();
        pos_B = new List<int[]>();
        pos_C = new List<int[]>();
        pos_D = new List<int[]>();

        //Create();



        

        
        while(!success)
        {
            Debug.Log("yooooo");
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    List<TileType> candidate_assignments = new List<TileType> { TileType.A, TileType.B, TileType.C, TileType.D };
                    Shuffle<TileType>(ref candidate_assignments);

                    grid[i, j] = candidate_assignments;
                    unassigned.Add(new int[] { i, j });
                }
            }
            success = BackTrackingSearch(grid, unassigned, level);
            if (!success)
            {
                Debug.Log("Could not find valid solution - will try again");
                unassigned.Clear();
                grid = new List<TileType>[size, size];
                function_calls = 0; 
            }
            else{
                Debug.Log("Found");
                
            }
        }

        int wr = 0;
        int lr = 0;
        
        while (true) // try until a valid position is sampled
        {
            wr = UnityEngine.Random.Range(0, size);
            lr = UnityEngine.Random.Range(0, size);

            if (grid[wr, lr].Count == 1)
            {
                player_start = new int[] {wr, lr};
                
                player.SetActive(true);
                player.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                player.transform.position = new Vector3(lr*5 - (float)(size * Math.Floor(width/2)), -1.9f, wr*width - 2.0f);
                break;
            }
        }

        while (true) // try until a valid position is sampled
        {
            wr = UnityEngine.Random.Range(0, size);
            lr = UnityEngine.Random.Range(0, size);

            if (grid[wr, lr].Count == 1 && wr != player_start[0] && lr != player_start[1])
            {
                opponent_start = new int[] {wr, lr};

                //GameObject opp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject opp = Instantiate(opp_prefab);
                opp.name = "opp";
                opp.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                opp.transform.position = new Vector3(lr*5 - (float)(size * Math.Floor(width/2)), -1.9f, wr*width - 2.0f);
                opp.AddComponent<CharacterController>();
                CharacterController cc = opp.GetComponent<CharacterController>();
                cc.center = new Vector3(0.0f,1.0f, 0.0f);
                opp.AddComponent<BoxCollider>();
                opp.AddComponent<UnityEngine.AI.NavMeshAgent>();
                //Rigidbody gameObjectsRigidBody = opp.AddComponent<Rigidbody>(); 
                //gameObjectsRigidBody.mass = 0.1f;
                //opp.AddComponent<BoxCollider>();
                //BoxCollider b = opp.GetComponent<Collider>() as BoxCollider;
                //b.size = new Vector3(1.0f, 0.5f, 1.0f);
                break;
            }
        }

        int time_obj = 1;
        while (time_obj < 3) // try until a valid position is sampled
        {
            wr = UnityEngine.Random.Range(0, size);
            lr = UnityEngine.Random.Range(0, size);

            if (grid[wr, lr].Count == 1 && wr != player_start[0] && lr != player_start[1])
            {
                
                GameObject clock = Instantiate(myPrefab, new Vector3(lr*5 - (float)(size * Math.Floor(width/2)), 1.5f, wr*width - 2.0f), Quaternion.identity);
                clock.name = "clock" + time_obj.ToString();
                clock.transform.eulerAngles = new Vector3(-10f, 200f, 10f);
                BoxCollider boxCollider = clock.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;
                time_obj++;
            }
        }

        


        
        

        CheckIfPathExisits(ref grid, player_start[0], player_start[1], 1);
        CheckIfPathExisits(ref grid, opponent_start[0], opponent_start[1], 5);

        
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                
                if(grid[i,j].Count == 1)
                {



                    tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    tile.name = "TILE" + (i*size + j).ToString();
                    tile.transform.localScale = new Vector3(5.0f, 0.2f, 5.0f);
                    tile.transform.position = new Vector3(j*5 - (float)(size * Math.Floor(width/2)), -2, i*width - 2.0f);
                    //tile.GetComponent<Renderer>().material.color = Color.red;
                    tile.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off ;

                    if(grid[i, j][0] == TileType.A)
                        tile.GetComponent<Renderer>().material.color = Color.red;

                    else if(grid[i, j][0] == TileType.B)
                        tile.GetComponent<Renderer>().material.color = Color.blue;

                    else if(grid[i, j][0] == TileType.C)
                        tile.GetComponent<Renderer>().material.color = Color.yellow;

                    else if(grid[i, j][0] == TileType.D)
                        tile.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
        

    }

    bool BackTrackingSearch(List<TileType>[,] grid, List<int[]> unassigned, int level)
    {
       
        int u_count = 20;

        if (level == 1)
            u_count = 0;

        else if (level == 2)
            u_count = 20;

        else if (level == 3)
            u_count = 40;

        else if (level == 4)
            u_count = 70;

        else if (level == 5)
            u_count = 90;


        if (function_calls++ > 100000)       
            return false;

        
        if (unassigned.Count == u_count)
            return true;

        Shuffle<int[]>(ref unassigned);
        int[] cur = unassigned[0];
        int x = cur[0];
        int y = cur[1];
        List<TileType> candidates = grid[x,y]; 
        for (int i = 0; i < candidates.Count; i++)
        {
            if (CheckConsistency(grid, cur, candidates[i], level)) 
            {
                grid[x,y] = new List<TileType> { candidates[i] };
                unassigned.RemoveAt(0);
                if (candidates[i] == TileType.A) 
                    pos_A.Insert(0, new int[] { x, y });

                else if (candidates[i] == TileType.B) 
                    pos_B.Insert(0, new int[] { x, y });

                else if (candidates[i] == TileType.C) 
                    pos_C.Insert(0, new int[] { x, y });

                else if (candidates[i] == TileType.D) 
                    pos_D.Insert(0, new int[] { x, y });

                if(BackTrackingSearch(grid, unassigned, level))
                {
                    return true;
                }

                unassigned.Insert(0, cur);
                grid[x,y] = candidates;

                if (candidates[i] == TileType.A) 
                    pos_A.RemoveAt(0);

                else if (candidates[i] == TileType.B) 
                    pos_B.RemoveAt(0);

                else if (candidates[i] == TileType.C) 
                    pos_C.RemoveAt(0);

                else if (candidates[i] == TileType.D) 
                    pos_D.RemoveAt(0);
            }
        }
        return false;


    }

    bool CheckConsistency(List<TileType>[,] grid, int[] cell_pos, TileType t, int level)
    {
        int limit = 30;

        if (level == 2)
            limit = 23;

        else if (level == 3)
            limit = 18;

        else if (level == 4)
            limit = 9;

        else if (level == 5)
            limit = 3;

        int i = cell_pos[0];
        int j = cell_pos[1];

        
        

        bool areWeConsistent = pos_A.Count <= limit && pos_B.Count <= limit && pos_C.Count <= limit && pos_D.Count <= limit;
        
        

        return areWeConsistent;
    }

    

    int heuristic (Tile x, Tile goal) {
        return Mathf.Abs(goal.row - x.row) + Mathf.Abs(goal.col - x.col);
    }

    void AddNeighbor(ref List<Tile> tile_list, ref List<Tile> visited, Tile cur_tile, Tile goal, int row, int col, int is_empty = 0)
    {    
        foreach (Tile i in visited)
        {
            if (i.row == row && i.col == col)
                return;

        }
        Tile neighbor = new Tile(row, col);
        if(is_empty == 0)
        {
            int gscore = cur_tile.gscore + 1;
            if (gscore < neighbor.gscore)
            {
                neighbor.gscore = gscore;
                neighbor.fscore = neighbor.gscore + heuristic(neighbor, goal);
                neighbor.route.AddRange(cur_tile.route);
                neighbor.route.Add(cur_tile);
                tile_list.Add(neighbor);
            }
            return;
        }

        int empty = cur_tile.empty;
        if (is_empty == 2)
            empty++;
        neighbor.empty = empty;
        neighbor.route.AddRange(cur_tile.route);
        neighbor.route.Add(cur_tile);
        tile_list.Add(neighbor);
 
    }

    bool isPathPresent(ref List<TileType> [,] grid, Tile source, Tile goal, int player)
    {
        List<Tile> tile_list = new List<Tile>();
        List<Tile> visited = new List<Tile>();
        source.gscore = 0;
        source.fscore = source.gscore + heuristic(source, goal);
        tile_list.Add(source);
        Tile cur_tile;
        int row;
        int col;
        int n = 0;
        while(tile_list.Count > 0){
            n++;
            cur_tile = tile_list[0];
            
            
            tile_list.RemoveAt(0);
            if (cur_tile.row == goal.row && cur_tile.col == goal.col) 
            {
                cur_tile.route.Add(cur_tile);
                assignRoute(cur_tile, player);
                return true;
            }

            visited.Add(cur_tile);

            //Sides
            row = cur_tile.row + 1;
            col = cur_tile.col;
            if(row < size || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row - 1;
            col = cur_tile.col;
            if(row >= 0 || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row;
            col = cur_tile.col + 1;
            if(col < size  || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row;
            col = cur_tile.col - 1;
            if(col >= 0 || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            if (player == 1)
            {
                //Sides with Jump
                row = cur_tile.row + 2;
                col = cur_tile.col;
                if(row < size || (row == goal.row && col == goal.col))
                {
                    if (grid[row, col].Count == 1)
                        AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
                }

                row = cur_tile.row - 2;
                col = cur_tile.col;
                if(row >= 0 || (row == goal.row && col == goal.col))
                {
                    if (grid[row, col].Count == 1)
                        AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
                }

                row = cur_tile.row;
                col = cur_tile.col + 2;
                if(col < size  || (row == goal.row && col == goal.col))
                {
                    if (grid[row, col].Count == 1)
                        AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
                }

                row = cur_tile.row;
                col = cur_tile.col - 2;
                if(col >= 0 || (row == goal.row && col == goal.col))
                {
                    if (grid[row, col].Count == 1)
                        AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
                }
            }

            //Diagonals
            row = cur_tile.row - 1;
            col = cur_tile.col - 1;
            if((col >= 0 && row >= 0)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row - 1;
            col = cur_tile.col + 1;
            if((row >= 0 && col < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row + 1;
            col = cur_tile.col - 1;
            if((col >= 0 && row < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }

            row = cur_tile.row + 1;
            col = cur_tile.col + 1;
            if((row < size && col < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count == 1)
                    AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col);
            }



            tile_list.Sort(delegate (Tile a, Tile b)
            {
                return (a.fscore).CompareTo(b.fscore);
            });

            


        }
        return false;


    }


    void Create_Path(ref List<TileType> [,] grid,  Tile source, Tile goal, int player)
    {
        List<Tile> tile_list = new List<Tile>();
        List<Tile> visited = new List<Tile>();
        
        source.gscore = 0;
        source.empty = 0;
        tile_list.Add(source);
        Tile cur_tile;
        int row;
        int col;
        int n = 0;
        int is_empty;

        while(tile_list.Count > 0)
        {
            n++;
            cur_tile = tile_list[0];
            
            
            tile_list.RemoveAt(0);
            if (cur_tile.row == goal.row && cur_tile.col == goal.col) 
            {
                
                foreach (Tile x in cur_tile.route) 
                {
                    if (grid[x.row, x.col].Count > 1) 
                    {
                        Debug.Log("Adding tile at "+ x.row + ", " + x.col);
                        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        tile.name = "TILE" + (x.row*size + x.col).ToString();
                        tile.transform.localScale = new Vector3(5.0f, 0.2f, 5.0f);
                        tile.transform.position = new Vector3(x.col*5 - (float)(size * Math.Floor(width/2)), -2, x.row*width - 2.0f);
                        tile.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off ;

                        grid[x.row, x.col] =  new List<TileType> {grid[x.row, x.col][0]};
                        if(grid[x.row, x.col][0] == TileType.A)
                            tile.GetComponent<Renderer>().material.color = Color.red;

                        else if(grid[x.row, x.col][0] == TileType.B)
                            tile.GetComponent<Renderer>().material.color = Color.blue;

                        else if(grid[x.row, x.col][0] == TileType.C)
                            tile.GetComponent<Renderer>().material.color = Color.yellow;

                        else if(grid[x.row, x.col][0] == TileType.D)
                            tile.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
                cur_tile.route.Add(cur_tile);
                assignRoute(cur_tile, player);
                return;
            }

            visited.Add(cur_tile);

            row = cur_tile.row + 1;
            col = cur_tile.col;
            if(row < size || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }

            row = cur_tile.row - 1;
            col = cur_tile.col;
            if(row >= 0 || (row == goal.row && col == goal.col))
            {
                 
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }
            

            row = cur_tile.row;
            col = cur_tile.col + 1;
            if(col < size || (row == goal.row && col == goal.col))
            {
                 
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }
            

            row = cur_tile.row;
            col = cur_tile.col - 1;
            if(col >= 0 || (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }

            row = cur_tile.row - 1;
            col = cur_tile.col - 1;
            if((col >= 0 && row >= 0)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }

            row = cur_tile.row - 1;
            col = cur_tile.col + 1;
            if((row >= 0 && col < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }

            row = cur_tile.row + 1;
            col = cur_tile.col - 1;
            if((col >= 0 && row < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }

            row = cur_tile.row + 1;
            col = cur_tile.col + 1;
            if((row < size && col < size)|| (row == goal.row && col == goal.col))
            {
                if (grid[row, col].Count > 1)
                    is_empty = 2;
                else
                    is_empty = 1;
                AddNeighbor(ref tile_list, ref visited, cur_tile, goal, row, col, is_empty);
            }




            tile_list.Sort(delegate (Tile a, Tile b)
            {
                return (a.empty).CompareTo(b.empty);
            });

        }


    }

    void assignRoute(Tile tile, int player)
    {
        if(player == 1)
            player_to_A = tile.route;

        if(player == 2)
            player_to_B = tile.route;

        if(player == 3)
            player_to_C = tile.route;

        if(player == 4)
            player_to_D = tile.route;

        if(player == 5)
            opponent_to_A = tile.route;

        if(player == 10)
            opponent_to_B = tile.route;

        if(player == 15)
            opponent_to_C = tile.route;

        if(player == 20)
            
            opponent_to_D = tile.route;
    }

    



    void CheckIfPathExisits(ref List<TileType> [,] grid, int x, int y, int player)
    {
        
        Tile source = new Tile(x, y);
        Tile goal;
        
        int i;
        int h = 0;
        for (i = 0; i< pos_A.Count; i++)
        {   
            h = 0;
            goal = new Tile(pos_A[i][0], pos_A[i][1]);
            if (isPathPresent(ref grid, source, goal,  1 * player)){
                h = 1;
                
                break;
            }
        }
        if (h == 0)
        {
            goal = new Tile(pos_A[0][0], pos_A[0][1]);
            Create_Path(ref grid, source, goal, 1 * player);
        }



         for (i = 0; i< pos_B.Count; i++)
        {   
            h = 0;
            goal = new Tile(pos_B[i][0], pos_B[i][1]);
            if (isPathPresent(ref grid, source, goal, 2 * player)){
                h = 1;
                break;
            }
        }
        if (h == 0)
        {
            goal = new Tile(pos_B[0][0], pos_B[0][1]);
            Create_Path(ref grid, source, goal, 2 * player);
        }


         for (i = 0; i< pos_C.Count; i++)
        {   
            h = 0;
            goal = new Tile(pos_C[i][0], pos_C[i][1]);
            if (isPathPresent(ref grid, source, goal, 3 * player)){
                h = 1;
                break;
            }
        }
        if (h == 0)
        {
            goal = new Tile(pos_C[0][0], pos_C[0][1]);
            Create_Path(ref grid, source, goal, 3 * player);
        }


         for (i = 0; i< pos_D.Count; i++)
        {   
            h = 0;
            goal = new Tile(pos_D[i][0], pos_D[i][1]);
            if (isPathPresent(ref grid, source, goal, 4 * player)){
                h = 1;
                break;
            }
        }
        if (h == 0)
        {
            goal = new Tile(pos_D[0][0], pos_D[0][1]);
            Create_Path(ref grid, source, goal,  4 * player);
        }
    }



    void Create()
    {
        int i = 0;
        int j = 0;
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = "TILE" + (i*size + j).ToString();
                tile.transform.localScale = new Vector3(5.0f, 0.2f, 5.0f);
                tile.transform.position = new Vector3(j*5 - (float)(size * Math.Floor(width/2)), -2, i*width - 2.0f);
                tile.GetComponent<Renderer>().material.color = Color.red;
                tile.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off ;
                
                
                
            }
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile 
{
    public int row;
    public int col;
    public int gscore;
    public int fscore;
    public int empty;
    public List<Tile> route;

    public Tile (int row, int col) {
        this.row = row;
        this.col = col;
        this.empty = 0;
        this.gscore = 128;
        this.fscore = 0;
        this.route = new List<Tile>();
    }
}

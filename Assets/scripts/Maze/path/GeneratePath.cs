using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generating maze path
public class GeneratePath
{
    //starting pos
    private int startX;
    private int startY;

    private Grid grid;

    //constructor
    public GeneratePath(int _x, int _y, Grid _grid)
    {
        this.startX = _x;
        this.startY = _y;

        this.grid = _grid;
    }

    public void startGenerator()
    {
        
    }

    public Grid getGrid()
    {
        return grid;
    }

}

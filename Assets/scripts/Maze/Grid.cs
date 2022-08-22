using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that contains the data of the grid and its cells
public class Grid
{
    public int width;
    public int height;

    public Cell[][] cells;

    //i chose to make more stacks so i can make it more like a tree
    public List<int[][]> stacks = new List<int[][]>();
    public Grid(int _width, int _height) 
    {
        this.width = _width;
        this.height = _height;
    }

}

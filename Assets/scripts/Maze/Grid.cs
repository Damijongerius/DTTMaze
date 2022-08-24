using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that contains the data of the grid and its cells
public class Grid
{
    public int width;
    public int height;

    public Cell[,] cells;

    //i chose to make more stacks so i can make it more like a tree
    //in these stacks im going to save the last done moves
    public List<int[,]> stacks = new List<int[,]>();
    public int checkmarks = new int();
    public Grid(int _width, int _height) 
    {
        this.width = _width;
        this.height = _height;
    }

    public void init()
    {
        this.cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell(x, y);
                this.cells[x, y] = cell;
            }
        }
    }
}

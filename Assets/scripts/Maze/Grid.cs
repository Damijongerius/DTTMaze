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
    public List<List<Vector2>> stacks = new List<List<Vector2>>();
    public int checkmarks = 1;
    public Grid(int _width, int _height) 
    {
        this.width = _width;
        this.height = _height;
    }

    public void init(Vector2 start, Vector2 end)
    {
        this.cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell(x, y);
                
                //testing if its the start or end pos
                if(x == start.x)
                {
                    if(y == start.y)
                    {
                        cell.start = true;
                    }
                }

                if (x == end.x)
                {
                    if (y == end.y)
                    {
                        cell.end = true;
                    }
                }

                this.cells[x, y] = cell;
            }
        }
    }

    public bool allVisited()
    {
        if(checkmarks == width * height)
        {
            return true;
        }
        return false;
    }
}

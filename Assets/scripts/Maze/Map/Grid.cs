using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that contains the data of the grid and its cells
public class Grid
{
    public int width;
    public int height;

    private Type type;

    public Cell[,] cells;

    //i chose to make more stacks so i can make it more like a tree
    //in these stacks im going to save the last done moves
    public List<List<Vector2>> stacks = new List<List<Vector2>>();
    public int checkmarks = 1;
    public int needed;

    private static Grid instance;

    public Grid(int _width, int _height) 
    {
        (this.width, this.height) = (_width, _height);
        instance = this;
    }

    public void init(Vector2 _start, Vector2 _end, Type _type)
    {
        this.cells = new Cell[width, height];
        this.type = _type;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell(x, y);
                
                //testing if its the start or end pos
                if(x == _start.x && y == _start.y) cell.start = true;
                if (x == _end.x && y == _end.y) cell.end = true;
                this.cells[x, y] = cell;
            }
        }

        //random generator
        if (_type == Type.PERLINNOISE)
        {
            //running function in PerlinNoise
            PerlinNoise pn = new PerlinNoise(this);
            this.cells = pn.Randomize();
            needed = width * height;
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

    public static Grid GetInstance()
    {
        return instance;
    }
}

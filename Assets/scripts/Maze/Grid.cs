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
    public Grid(int _width, int _height) 
    {
        this.width = _width;
        this.height = _height;
    }

    public void init(Vector2 start, Vector2 end)
    {
        this.cells = new Cell[width, height];
        this.type = _type;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell(x, y);
                
                //testing if its the start or end pos
<<<<<<< Updated upstream:Assets/scripts/Maze/Grid.cs
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

=======
                if(x == _start.x && y == _start.y) cell.start = true;
                if (x == _end.x && y == _end.y) cell.end = true;
>>>>>>> Stashed changes:Assets/scripts/Maze/Map/Grid.cs
                this.cells[x, y] = cell;
            }
        }

        //random generator
        if (_type == Type.PERLINNOISE)
        {
            //running function in PerlinNoise
            PerlinNoise pn = new PerlinNoise(this);
            this.cells = pn.Randomize();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the cell that contains if its visited and wich wall is open and closed
public class Cell : CellBase
{
    //this constructor allows me to ask for some thing as a new cell is made
    public Cell(int x, int y)
    {
        this.y = y;
        this.x = x;
    }

    //this method is looking for neighbours that are not visited yet
    public List<Vector2> hasUnvisitedNeighbours(Grid grid)
    {
        List<Vector2> neighbours = new List<Vector2>();
        
        //for every direction checking for neighbour in grid
        try
        {
            if (grid.cells[(x + 1), y].visited == false)
            {
                neighbours.Add(new Vector2(x + 1, y));
            }

            if (grid.cells[x, (y + 1)].visited == false)
            {
                neighbours.Add(new Vector2(x, y + 1));
            }
            }
        catch
        {

        }

        if (x > 0)
        {
            if (grid.cells[(x - 1), y].visited == false)
            {
                neighbours.Add(new Vector2(x - 1, y));
            }
        }

        if (y > 0)
        {
            if (grid.cells[x, (y - 1)].visited == false)
            {
                neighbours.Add(new Vector2(x, y - 1));
            }
        }
        return neighbours;
    }
}




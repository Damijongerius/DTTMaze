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
    public bool hasUnvisitedNeighbours(Grid grid)
    {
        if (grid.cells[x + 1][y].visited == false)
        {
            return true;
        }else if (grid.cells[x - 1][y].visited == false)
        {
            return true;
        }else if (grid.cells[x][y + 1].visited != false)
        {
            return true;
        }else if (grid.cells[x][y - 1].visited != false)
        {
            return true;
        }
     return false;
    }
}




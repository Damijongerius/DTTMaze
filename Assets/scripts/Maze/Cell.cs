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
    public List<int[,]> hasUnvisitedNeighbours(Grid grid)
    {
        List<int[,]> neighbours = new List<int[,]>();
        if (grid.cells[x + 1,y].visited == false)
        {
           neighbours.Add(new int [x + 1, y]);
        }else if (grid.cells[x - 1,y].visited == false)
        {
            neighbours.Add(new int[x - 1, y]);
        }
        else if (grid.cells[x,y + 1].visited != false)
        {
            neighbours.Add(new int[x, y + 1]);
        }
        else if (grid.cells[x,y - 1].visited != false)
        {
            neighbours.Add(new int[x, y - 1]);
        }
     return neighbours;
    }
}




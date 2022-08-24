using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  23/08/2022
  i made this script so its easier letting everything generate
  the only thing generate path will need to do then is call walk
  and keep up the stack with checkmarks

*/
public class Path
{
    //path history
    private List<int[,]> stack = new List<int[,]>();

    //parent script
    private GeneratePath generator;

    //position
    private int x;
    private int y;

    //constructor
    public Path(GeneratePath _generator, int x, int y)
    {
        this.generator = _generator;

        //this is so the script knows where its starting
        this.x = x;
        this.y = y;
    }

    public bool walk()
    {
        Grid grid = generator.getGrid();
        if (grid.cells[x, y].hasUnvisitedNeighbours(grid) == null)
        {
            List<int[,]> neighbours = grid.cells[x, y].hasUnvisitedNeighbours(grid);
            Cell cell;
            for(int i = 0; i < neighbours.Count; i++)
            {
                if(Random.Range(0, 1) <= 0.5)
                {
                    cell = grid.cells[neighbours[i].GetLength(0), neighbours[i].GetLength(1)];
                    newPos(cell);
                    break;
                }
            }          
        }

        return false;

        void newPos(Cell cell)
        {
            grid.cells[x, y].visited = true;

            this.x = cell.x;
            this.y = cell.y;
            cell.visited = true;
        }
    }
}

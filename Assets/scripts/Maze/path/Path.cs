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
    private List<Vector2> stack = new List<Vector2>();

    //starting pos
    private Vector2 start;

    //parent script
    private readonly GeneratePath generator;

    //position
    private int x;
    private int y;

    public bool delete = false;

    //stack list location
    private int index = 0;

    //chance of mutating
    private int chance;

    //constructor
    public Path(GeneratePath _generator, int _x, int _y, List<Vector2> _stack)
    {
        this.generator = _generator;

        //this is so the script knows where its starting
        (this.x, this.y) = (_x, _y);

        //this is for the stack backtracking so i know where it begon so it wont go furthur
        this.start = new Vector2(x, y);

        //if im creating a new path
        if (_stack != null)
            this.stack = _stack;

    }

    //the movement of the script
    public Path Walk()
    {
        // getting the grid so i can get the cells
        Grid grid = generator.getGrid();
        Cell cell;


        //asking for the unvisited neighbours
        List<Vector2> neighbours;
        try
        {
            neighbours = grid.cells[x, y].hasUnvisitedNeighbours(grid);
        }
        catch
        {
            neighbours = new List<Vector2>();
        }


        //testing if their are unvisited neighbours in the list
        //and if it issnt null it will randomly choose a neighbour
        //Debug.Log(neighbours.Count);
        if (neighbours.Count != 0)
        {
            int random = Random.Range(0, neighbours.Count);

            cell = grid.cells[(int)neighbours[random].x, (int)neighbours[random].y];
            walking(cell);
        }
        else
        {
            //if there are not possibilities here it will go back and try to do it again
            //Debug.Log("no possible neighbour");
            if (walkBack())
            {
                return this;
            }
            else
            {
                Walk();
            }
        }


        void walking(Cell cell)
        {
            //first making sure visited it true
            //adding the last pos to stack
            //and removing the walls
            grid.cells[x, y].visited = true;
            stack.Add(new Vector2(x, y));

            removeWall(cell);
            Debug.Log(index + "walking from:" + x + "," + y + " to:" + cell.x + "," +  cell.y);

            //setting the new positions 
            //and adding a checkmark so the code later will see ooh everything send back a mark so i can stop generating
            this.x = cell.x;
            this.y = cell.y;
            
            cell.visited = true;
            grid.checkmarks++;

            if (index == 0)
            {
                grid.stacks.Add(stack);
                index = grid.stacks.Count;
            }
            else
            {
                grid.stacks[index] = stack;
            }
        }

        //letting the class go back to an older pos
        bool walkBack()
        {
            int length = stack.Count - 1;

            //if its at its start it will delete itself
            //otherwise it will just return and run again
            if(stack[length] != start)
            {
                Debug.Log(index + "walkingback from:" + x + "," + y + " to:" + (int)stack[length].x + "," + (int)stack[length].y);
                this.x = (int)stack[length].x;
                this.y = (int)stack[length].y;
                stack.RemoveAt(length);

                return false;
            }
            else
            {
                this.delete = true;
                return true;
            }
        }

        void removeWall(Cell cell)
        {
            //getting the axis that it has moved
            int diffX = x - cell.x;
            int diffY = y - cell.y;


            //looking wich axis has moves
            if(diffX != 0)
            {
                //preventing the generation on the boolean walls
                switch (diffX)
                {
                    case -1:
                        {
                            grid.cells[x, y].walls[1] = false;
                            cell.walls[2] = false;
                            break;
                        }
                    case 1:
                        {
                            grid.cells[x, y].walls[2] = false;
                            cell.walls[1] = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //preventing the generation on the boolean walls
                switch (diffY)
                {
                    case -1:
                        {
                            grid.cells[x, y].walls[3] = false;
                            cell.walls[4] = false;
                            break;
                        }
                    case 1:
                        {
                            grid.cells[x, y].walls[4] = false;
                            cell.walls[3] = false;
                            break;
                        }
                }
            }
        }
        return null;
    }
}

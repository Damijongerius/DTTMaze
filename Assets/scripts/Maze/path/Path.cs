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
    public List<Vector2> history = new List<Vector2>();

    //starting pos
    private Vector2 start;

    //parent script
    private readonly GeneratePath generator;

    //position
    public int x;
    public int y;

    public bool delete = false;

    //chance of mutating
    private int chance;

    //death wish
    private Vector2 portal = new Vector2(0,0);
    private Vector2 portalTo = new Vector2(0, 0);

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
    public void Walk()
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

        //if the cell is at a place it can't walk most likly in the beginning it will teleport to a random place until its not anymore
        if (grid.cells[x, y].Use == false)
        {
            grid.cells[x, y].start = false;
            this.x = Random.Range(0, grid.width);
            this.y = Random.Range(0, grid.height);

            if(grid.cells[x, y].Use == true && grid.cells[x, y].end == false)
            {
                grid.cells[x, y].start = true;
                start = new Vector2(x, y);
                MazeGenerator.start = start;
            }
        }


        //testing if their are unvisited neighbours in the list
        //and if it issnt null it will randomly choose a neighbour
        if (neighbours.Count != 0)
        {
            int random = Random.Range(0, neighbours.Count);

            cell = grid.cells[(int)neighbours[random].x, (int)neighbours[random].y];

            walking(cell);
        }
        else
        {
            //if there are not possibilities here it will go back and try to do it again
            if (walkBack())
            {
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
            history.Add(new Vector2(x, y));
            portal = history[Random.Range(0, history.Count)];

            removeWall(cell);

            //setting the new positions 
            //and adding a checkmark so the code later will see ooh everything send back a mark so i can stop generating
            this.x = cell.x;
            this.y = cell.y;
            
            cell.visited = true;
            grid.checkmarks++;

            if (history.Contains(start))
            {
                history.Remove(start);
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
                this.x = (int)stack[length].x;
                this.y = (int)stack[length].y;
                stack.RemoveAt(length);

                return false;
            }
            else
            {
                //setting the purple portals from place to place
                if(portalTo == new Vector2(0, 0))
                {
                    foreach(Cell cell in grid.cells)
                    {
                        if(cell.visited == false &&  cell.Use == true)
                        {
                            //setting portals
                            portalTo = new Vector2(cell.x, cell.y);
                            cell.IsPortal = true;
                            cell.ConnectPoint = portal;

                            grid.cells[(int)portal.x, (int)portal.y].IsPortal = true;
                            grid.cells[(int)portal.x, (int)portal.y].ConnectPoint = new Vector2(cell.x, cell.y);

                            Path path = new Path(generator, cell.x, cell.y, stack);
                            generator.AddPath(path);
                            break;
                        }
                    }
                }
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
    }
}

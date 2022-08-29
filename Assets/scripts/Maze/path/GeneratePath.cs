using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//generating maze path
public class GeneratePath
{
    //starting pos
    private int startX;
    private int startY;

    private Grid grid;

    private List<Path> pathList = new List<Path>();
    private List<Path> pathListInactive = new List<Path>();

    private int a;

    //constructor
    public GeneratePath(int _x, int _y, Grid _grid)
    {
        (this.startX, this.startY) = (_x, _y);

        this.grid = _grid;

        Path path = new Path(this, startX, startY, null);
        pathList.Add(path);
    }

    //generating the maze
    public void startGenerator()
    {
        //aslong as it doessn't have generated the full maze it wil continue calculating
        if (grid.checkmarks < grid.width * grid.height)
        {
            //this try catch is for the changing of the list impacting the foreach
            try
            {
                a++;
                foreach (Path p in pathList)
                {
                    if (p.delete)
                    {
                        if(pathList.Count == 1)
                        {
                            Vector2 end = p.history[Random.Range(0, p.history.Count)];
                            grid.cells[(int)end.x, (int)end.y].end = true;

                            MazeGenerator mg = MazeGenerator.getInstance();
                            mg.end = end;
                        }
                        pathListInactive.Add(p);
                        pathList.Remove(p);
                    }
                    if(a % 1 == 0)
                    {
                        p.Walk();
                    }
                }   
            }
            catch
            {
                return;
            }
        }
        //if its done it will stop looping
        else
        {
            Debug.Log("done");
            MazeGenerator mg = MazeGenerator.getInstance();
            mg.updating = null;
        }
    }

    //grid for path
    public Grid getGrid()
    {
        return grid;
    }

    //adding path if needed
    public void AddPath(Path newPath)
    {
         pathList.Add((Path)newPath);
    }

}

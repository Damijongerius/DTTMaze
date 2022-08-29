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
                foreach (Path p in pathList)
                {
                    if (p.delete)
                    {
                        if(pathList.Count == 1)
                        {
                            Vector2 end = p.history[Random.Range(0, p.history.Count)];
                            Vector2 end2 = MazeGenerator.end;
                            Debug.Log(" " + end2 + " == "  + (grid.width - 1) + "," + (grid.height - 1));
                            if (MazeGenerator.end == new Vector2(grid.width -1, grid.height -1) && grid.cells[grid.width - 1, grid.height - 1].Use == false)
                            {
                                grid.cells[(int)end.x, (int)end.y].end = true;
                                Debug.Log(MazeGenerator.end);
                                MazeGenerator.end = end;
                            }
                        }
                        pathListInactive.Add(p);
                        pathList.Remove(p);
                    }
                        p.Walk();

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

            if (pathList.Count == 1)
            {
                Vector2 end = pathList[0].history[Random.Range(0, pathList[0].history.Count)];
                Vector2 end2 = MazeGenerator.end;
                if (MazeGenerator.end == new Vector2(grid.width - 1, grid.height - 1) && grid.cells[grid.width - 1, grid.height - 1].Use == false)
                {
                    grid.cells[(int)end.x, (int)end.y].end = true;
                    Debug.Log(MazeGenerator.end);
                    MazeGenerator.end = end;
                }
            }
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

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
        this.startX = _x;
        this.startY = _y;

        this.grid = _grid;

        Path path = new Path(this, startX, startY, null);
        pathList.Add(path);
    }

    public void startGenerator()
    {
        if (grid.checkmarks < grid.width * grid.height)
        {
            try
            {
                foreach (Path p in pathList)
                {
                    if (p.delete)
                    {
                        Debug.Log("removing path amount:" + pathList.Count);
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
        else
        {
            MazeGenerator mg = MazeGenerator.getInstance();
            mg.updating = null;
        }
    }

    public Grid getGrid()
    {
        return grid;
    }

    public void AddPath(Path newPath)
    {
        Debug.Log("adding path amount:" + pathList.Count);
         pathList.Add((Path)newPath);
    }

}

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

    List<Path> pathList = new List<Path>();

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
            Debug.Log(grid.checkmarks + "| | | |" + grid.width * grid.height);
            try
            {
                foreach (Path p in pathList)
                {
                    Path newPath = p.Walk();
                    if (newPath != null)
                    {
                        if (newPath.delete)
                        {
                            pathList.Remove(newPath);
                        }
                        else
                        {
                            pathList.Add((Path)newPath);
                        }
                    }
                }
            }
            catch
            {
                return;
            }

        }
    }

    public Grid getGrid()
    {
        return grid;
    }

}

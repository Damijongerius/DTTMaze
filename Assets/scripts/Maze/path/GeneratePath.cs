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

    public void startGenerator()
    {
        if (grid.checkmarks < grid.width * grid.height)
        {
            //Debug.Log(grid.checkmarks + "| | | |" + grid.width * grid.height);
            try
            {
                foreach (Path p in pathList)
                { 
                    Path newPath = p.Walk();
                    if (newPath != null)
                    {
                        if (newPath.delete)
                        {
                            Debug.Log("removing path amount:" + pathList.Count);
                            pathListInactive.Add(newPath);
                            pathList.Remove(newPath);
                        }
                        else
                        {
                            Debug.Log("adding path amount:" + pathList.Count);
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
        else
        {
            MazeGenerator mg = MazeGenerator.getInstance();
            mg.updating = null;
            mg.updating += mg.generateTerrain;

            mg.startDrawing = true;
        }
    }

    public Grid getGrid()
    {
        return grid;
    }

}

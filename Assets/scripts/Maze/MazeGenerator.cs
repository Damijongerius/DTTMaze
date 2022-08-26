using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
  8/22/2022
  I am going to use the LiFo(last in first out) algorithm. 
  But im going try making a faster version that is able to 
  generate a maze on the same speed with a much larger size maze.
*/
public class MazeGenerator : MonoBehaviour
{
    //update delegate for running generatePath
    public delegate void Updating();
    public Updating updating;

    public Material floor;
    public GameObject empty;

    //these are the start positions
    public int startX;
    public int startY;

    private static MazeGenerator instance;

    public bool startDrawing = false;

    public List<List<Vector2>> vector2s = new List<List<Vector2>>();

    //here is the grid class containing everything about the maze
    private Grid grid;

    DrawMaze drawMaze;
    public MazeGenerator()
    {
        instance = this;
    }

    public void Generator(int width, int height)
    {
        grid = new Grid(width, height);
        grid.init();
        generatePath();
    }

    private void Update()
    {    
        if(updating != null)
        {
            updating();
        }
    }

    public void generatePath()
    {
        GeneratePath generatePath = new GeneratePath(startX,startY,grid);
        updating += generatePath.startGenerator;
    }
    //this will make the maze visible by making a mesh out of it
    public void generateTerrain()
    {
        if(startDrawing == true)
        {
            updating = null;
            startDrawing = false;
            if (drawMaze != null)
            {
                List<GameObject> destroy = drawMaze.getMeshes();
                foreach (GameObject go in destroy)
                {
                    Destroy(go);
                }
            }
            drawMaze = new DrawMaze(grid, floor, empty);
            drawMaze.DrawTerrain();
        }
    }

    public static MazeGenerator getInstance()
    {
        return instance;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
  8/22/2022
  I am going to use the LiFo(last in first out) algorithm. 
  But im going try making a faster version that is able to 
  generate a maze on the same speed with a much larger size maze.
*/
public class MazeGenerator : MonoBehaviour
{
    public Material floor;
    public GameObject empty;

    public int width;
    public int height;

    //these are the start positions
    public int startX;
    public int startY;
    

    //here is the grid class containing everything about the maze
    private static Grid grid;
    void Start()
    {
        grid = new Grid(width,height);
        grid.init();
        generatePath();
        generateTerrain();
    }

    public void generatePath()
    {

    }
    //this will make the maze visible by making a mesh out of it
    public void generateTerrain()
    {
        DrawMaze drawMaze = new DrawMaze(grid,floor, empty);
        drawMaze.DrawTerrain();
    }

}

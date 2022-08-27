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

    //these are the start and end positions
    public Vector2 start;
    public Vector2 end;

    private static MazeGenerator instance;

    public List<List<Vector2>> vector2s = new List<List<Vector2>>();

    //loop vector
    private Vector2 ivJv = new Vector2(0,0);

    //here is the grid class containing everything about the maze
    private Grid grid;
    private DrawMaze drawMaze;



    public MazeGenerator()
    {
        instance = this;
    }

    //generator starting everything to make the maze
    public void Generator(int _width, int _height)
    {
        end = new Vector2(_width -1,_height -1);
        grid = new Grid(_width, _height);
        grid.init(start,end,Type.SQUARE);
        if (drawMaze == null)
        {
            InvokeRepeating("generateTerrain", 0.0001f, 0.0001f);
        }
        else
        {
            for(int i = 0; i < drawMaze.objects.GetLength(0); i++)
            {
                for(int j = 0; j < drawMaze.objects.GetLength(1); j++)
                {
                    Destroy(drawMaze.objects[i,j]);
                }
            }
            ivJv = new Vector2(0, 0);

        }
        drawMaze = new DrawMaze(grid, floor, empty);
        generatePath();
    }



    private void Update()
    {
        //running updat if there is something in updating
        if(updating != null)
        {
            updating();
        }
    }

    //generatring the path and adding it to update so it can do its whole generation
    public void generatePath()
    {
        GeneratePath generatePath = new GeneratePath((int)start.x,(int)start.y,grid);
        updating += generatePath.startGenerator;
    }
    //this will make the maze visible by making a mesh out of it
    public void generateTerrain()
    {
        ivJv = drawMaze.DrawTerrain((int)ivJv.x, (int)ivJv.y);
    }

    //maze generator instance to get acces to everything in here from other scripts
    public static MazeGenerator getInstance()
    {
        return instance;
    }


}

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
    public int width;
    public int height;

    private static Grid grid;
    void Start()
    {
        
    }

    public Grid getGrid()
    {
        return grid;
    }
}

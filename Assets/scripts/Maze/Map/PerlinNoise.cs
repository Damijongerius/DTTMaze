using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  26/08/2022
  i wanted to have islands of mazes and connect them with portals
  cus i thought it was funny
*/
public class PerlinNoise
{
    //grid and width height
    private Grid grid;
    private Vector2 widthHeight;

    //constructor
    public PerlinNoise(Grid _grid)
    {
        this.grid = _grid;
        this.widthHeight = new Vector2(_grid.width, _grid.height);
    }

    //comparing values to determ if it needs to be used or not
    public Cell[,] Randomize()
    {
        //the impact of width and height
        float size = 0.0001315f;

        //value of actual size
        float start;
        if(widthHeight.x < 1000 && widthHeight.y < 1000)
        {
            start = 0.11f - (size * (widthHeight.x + widthHeight.y));
        }
        else
        {
            start = 0.11f - (size * (widthHeight.x / 3 + widthHeight.y / 3));
        }


        float[,] noiseMap = GenerateNoiseMap(start);
        //400 0.04
        //20 0.09

        //setting everything below 0.5 to nothing
        for (int x = 0; x < widthHeight.x; x++)
        {
            for (int y = 0; y < widthHeight.y; y++)
            {
                if (noiseMap[x,y] <= 0.5f) grid.cells[x, y].Use = false;
            }
        }
        Filter();
        return grid.cells;
    }

    //generatring the noise map
    public float[,] GenerateNoiseMap(float _scale)
    {
        float[,] noiseMap = new float[(int)widthHeight.x, (int)widthHeight.y];

        //random noise position
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

        for (int x = 0; x < (int)widthHeight.x; x++)
        {
            for (int y = 0; y < (int)widthHeight.y; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * _scale + xOffset, y * _scale + yOffset);
            }
        }
        return noiseMap;
    }

    public void Filter()
    {
        for (int x = 0; x < (int)widthHeight.x; x++)
        {
            for (int y = 0; y < (int)widthHeight.y; y++)
            {
                if (grid.cells[x, y].Use == true)
                {
                    List<Vector2> neighbours = grid.cells[x, y].hasUnvisitedNeighbours(grid);

                    if(neighbours.Count <= 1)
                    {
                        grid.cells[x, y].Use = false;
                    }
                }
            }
        }
    }
}

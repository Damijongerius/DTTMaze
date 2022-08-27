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
    private float[,] maskMap;

    private Grid grid;
    private Vector2 widthHeight;

    public PerlinNoise(Grid _grid)
    {
        this.grid = _grid;
        this.widthHeight = new Vector2(_grid.width, _grid.height);
    }
    public Cell[,] Randomize()
    {
        float size = 0.0001315f;
        float start;
        if(widthHeight.x < 1000 && widthHeight.y < 1000)
        {
            start = 0.11f - (size * (widthHeight.x + widthHeight.y));
        }
        else
        {
            start = 0.11f - (size * (widthHeight.x / 3 + widthHeight.y / 3));
        }
        Debug.Log(start);


        float[,] noiseMap = GenerateNoiseMap(start);
        //400 0.04
        //20 0.09

        for (int x = 0; x < widthHeight.x; x++)
        {
            for (int y = 0; y < widthHeight.y; y++)
            {
                if (noiseMap[x,y] <= 0.5f) grid.cells[x, y].Use = false;
            }
        }
        return grid.cells;
    }

    public float[,] GenerateNoiseMap(float _scale)
    {
        float[,] noiseMap = new float[(int)widthHeight.x, (int)widthHeight.y];
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
    public void Mask()
    {
        maskMap = new float[(int)widthHeight.x, (int)widthHeight.y];

        for (int x = 0; x < widthHeight.x; x++)
        {
            for (int y = 0; y < widthHeight.y; y++)
            {
               // maskMap[x, y] = 
            }
        }

    }
}

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
    private float[,] noiseMap;
    private float[,] maskMap;

    private Grid grid;
    private Vector2 widthHeight;

    public PerlinNoise(Grid _grid)
    {
        this.grid = _grid;
        this.widthHeight = new Vector2(_grid.width, _grid.height);
    }
    public void Randomize(float _scale)
    {
        GenerateNoiseMap(_scale);

        for (int x = 0; x < widthHeight.x; x++)
        {
            for (int y = 0; y < widthHeight.y; y++)
            {

            }
        }
    }

    public void GenerateNoiseMap( float _scale)
    {
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

        noiseMap = new float[(int)widthHeight.x, (int)widthHeight.y];

        for (int x = 0; x < widthHeight.x; x++)
        {
            for (int y = 0; y < widthHeight.y; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(x * _scale + xOffset, y * _scale + yOffset);
            }
        }
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

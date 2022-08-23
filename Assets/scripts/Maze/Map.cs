using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private int width;
    private int height;

    public Grid[,] grids;

    public Map(int _width,int _height)
    {
        this.width = _width;
        this.height = _height;
        grids = new Grid[_width,_height];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quads
{
    private int x;
    private int y;

    public quads(int x, int y)
    {
        this.x = x;
        this.y = y;

    }
    public Vector3[] Face(int face)
    {
        //corners of a square
        Vector3 a = new Vector3(x + .5f, .5f, y + .5f);
        Vector3 b = new Vector3(x - .5f, .5f, y + .5f);
        Vector3 c = new Vector3(x - .5f, -.5f, y + .5f);
        Vector3 d = new Vector3(x + .5f, -.5f, y + .5f);
        Vector3 e = new Vector3(x - .5f, .5f, y - .5f);
        Vector3 f = new Vector3(x + .5f, .5f, y - .5f);
        Vector3 g = new Vector3(x + .5f, -.5f, y - .5f);
        Vector3 h = new Vector3(x - .5f, -.5f, y - .5f);

        //faces of an inverted square
        Vector3[] floor = new Vector3[] { c, d, g, h, c, g };
        Vector3[] north = new Vector3[] { a, f, g, d, a, g };
        Vector3[] east  = new Vector3[] { c, b, a, c, a, d };
        Vector3[] south = new Vector3[] { h, e, b, h, b, c };
        Vector3[] west  = new Vector3[] { g, f, e, h, g, e };


        //return for the loop im running in DrawMaze
        switch (face)
        {
            case 0:
                return floor; 
            case 1:
                return north;
            case 2:
                return south;
            case 3:
                return east;
            case 4:
                return west;
            default:
                Debug.Log("fail");
                return null;
        }
    }
}

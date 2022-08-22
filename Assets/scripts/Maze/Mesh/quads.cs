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
        //bottom corners
        Vector3 a = new Vector3(x - .5f, 0, y + .5f);
        Vector3 b = new Vector3(x + .5f, 0, y + .5f);
        Vector3 c = new Vector3(x - .5f, 0, y - .5f);
        Vector3 d = new Vector3(x + .5f, 0, y - .5f);

        //top corners
        Vector3 e = new Vector3(x - .5f, 1f, y + .5f);
        Vector3 f = new Vector3(x + .5f, 1f, y + .5f);
        Vector3 g = new Vector3(x - .5f, 1f, y - .5f);
        Vector3 h = new Vector3(x + .5f, 1f, y - .5f);

        //faces
        Vector3[] floor = new Vector3[] { a, b, c, b, d, c };
        Vector3[] south = new Vector3[] { a, b, f, a, f, e };
        Vector3[] north = new Vector3[] { a, g, c, a, e, g };
        Vector3[] west  = new Vector3[] { f, b, a, e, f, a };
        Vector3[] east  = new Vector3[] { g, c, d, h, g, d };

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
                return null;
        }
    }
}

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
    public int width;
    public int height;
    public Faces face;

    //here is the grid class containing everything about the maze
    private static Grid grid;
    void Start()
    {
        grid = new Grid(width,height);
        grid.init();
        DrawTerrain(grid.cells);
        DrawTexture(grid.cells);
    }

    private void DrawTerrain(Cell[,] cells)
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = cells[x, y];

                quads quads = new quads(x ,y);
                Vector2[] uv = getUvs(cell);

                for (int k = 0; k < 5; k++)
                {
                    Vector3[] v = quads.Face(k);
                    for (int l = 0; l < 6; l++)
                    {
                        
                        vertices.Add(v[l]);
                        triangles.Add(triangles.Count);
                        uvs.Add(uv[l]);
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }

    private Vector2[] getUvs(Cell cell)
    {
           Vector2 uv00 = new Vector2(0, 0);
           Vector2 uv10 = new Vector2(1f, 0);
           Vector2 uv01 = new Vector2(1f, 1f);
           Vector2 uv11 = new Vector2(0, 1f);
           Vector2[] uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
           return uv;
    }

    void DrawTexture(Cell[,] grid)
    {

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = floor;
    }

    public Grid getGrid()
    {
        return grid;
    }
}

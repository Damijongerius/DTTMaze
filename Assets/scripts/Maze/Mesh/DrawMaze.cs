using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//in this script the maze wil get drawn
public class DrawMaze
{
    //defining all needs for creating meshes
    private int width;
    private int height;

    private Grid grid;

    private Material mat;
    private GameObject empty;

    //constructor
    public DrawMaze(Grid _grid, Material _mat, GameObject _gameObject)
    {
        this.width = _grid.width;
        this.height = _grid.height;

        this.empty = _gameObject;

        this.grid = _grid;
        this.mat = _mat;

        Debug.Log("constructor2 " + width + "\\" + height);
    }

    //create all the triangles,verts and uv's
    public void DrawTerrain()
    {
        for (int i = 0; i < CalculateAmount(width); i++)
        {
            for (int j = 0; j < CalculateAmount(height); j++)
            {
                
                GameObject obj = MazeGenerator.Instantiate(empty, new Vector3(i * 10, 0, j * 10), new Quaternion(0, 0, 0, 0));

                Mesh mesh = new Mesh();
                List<Vector3> vertices = new List<Vector3>();
                List<int> triangles = new List<int>();
                List<Vector2> uvs = new List<Vector2>();

                //for every grid position it will make a cube without a roof thats inverted
                for (int x = 0; x < CalculateLeftOver(width, i); x++)
                {
                    for (int y = 0; y < CalculateLeftOver(height, j); y++)
                    {
                        Cell cell = grid.cells[x, y];

                        quads quads = new quads(x, y);
                        Vector2[] uv = getUvs(cell);

                        //for 5 faces
                        for (int k = 0; k < 5; k++)
                        {
                            if (cell.walls[k] == false)
                            {
                                Vector3[] v = quads.Face(k);
                                //for 2 triangle corners
                                for (int l = 0; l < 6; l++)
                                {
                                    vertices.Add(v[l]);
                                    triangles.Add(triangles.Count);
                                    uvs.Add(uv[l]);
                                }
                            }
                        }
                    }
                }
                //setting all the gathered intel on mesh
                mesh.vertices = vertices.ToArray();
                mesh.triangles = triangles.ToArray();
                mesh.uv = uvs.ToArray();
                mesh.RecalculateNormals();

                MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
                meshFilter.mesh = mesh;

                MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();

                DrawTexture(obj);
            }
        }
    }

    //this method will simply calculate howmanytimes a new mesh will be created
    private int CalculateAmount(int _wH)
    {
        int answer = (int)Mathf.Ceil((float)_wH / 10);
        Debug.Log(answer);
        return answer;
    }

    //this method will calculate how many cells are going to be needed in one axis of a mesh
    private int CalculateLeftOver(int _wh, int _ij)
    {
        
        if ( _wh - (_ij * 10) < 10)
        {
            Debug.Log(_wh % 10);
            return _wh % 10;
        }
        return 10;
    }

    //just the basic uvs
    private Vector2[] getUvs(Cell cell)
    {
        //corners of material
        Vector2 uv00 = new Vector2(0, 0);
        Vector2 uv10 = new Vector2(1f, 0);
        Vector2 uv01 = new Vector2(1f, 1f);
        Vector2 uv11 = new Vector2(0, 1f);

        //pos for triangles
        Vector2[] uv = new Vector2[] { uv00, uv10, uv01, uv10, uv11, uv01 };
        return uv;
    }

    //puting a real texture on it 
    void DrawTexture(GameObject obj)
    {
        //puting the material on all the verts
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}

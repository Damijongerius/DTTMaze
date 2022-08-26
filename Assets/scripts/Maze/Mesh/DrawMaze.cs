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

    private List<GameObject> objects = new List<GameObject> ();

    //constructor
    public DrawMaze(Grid _grid, Material _mat, GameObject _gameObject)
    {
        this.width = _grid.width;
        this.height = _grid.height;

        this.empty = _gameObject;

        this.grid = _grid;
        this.mat = _mat;
    }

    //create all the triangles,verts and uv's
    public void DrawTerrain()
    {
        for (int i = 0; i < CalculateAmount(width); i++)
        {
            for (int j = 0; j < CalculateAmount(height); j++)
            {
                
                GameObject obj = MazeGenerator.Instantiate(empty, new Vector3(i * 10, 0, j * 10), new Quaternion(0, 0, 0, 0));
                objects.Add(obj);

                Mesh mesh = new Mesh();
                List<Vector3> vertices = new List<Vector3>();
                List<int> triangles = new List<int>();
                List<Vector2> uvs = new List<Vector2>();

                //for every grid position it will make a cube without a roof that has inverted faces
                for (int x = 0; x + (i * 10) < CalculateLeftOver(width, i) + (i * 10); x++)
                {
                    for (int y = 0; y + (j * 10) < CalculateLeftOver(height, j) + (j * 10); y++)
                    {
                        Cell cell = grid.cells[x + (i * 10), y + (j * 10)];

                        quads quads = new quads(x, y);
                        Vector2[] uv = getUvs(cell);

                        //for 5 faces
                        for (int k = 0; k < 5; k++)
                        {
                            if (cell.walls[k] == true)
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
        return answer;
    }

    //this method will calculate how many cells are going to be needed in one axis of a mesh
    private int CalculateLeftOver(int _wh, int _ij)
    {
        
        if ( _wh - (_ij * 10) < 10)
        {
            return _wh % 10;
        }
        return 10;
    }

    //just the basic uvs
    private Vector2[] getUvs(Cell cell)
    {
        Vector2 uv00;
        Vector2 uv10;
        Vector2 uv01;
        Vector2 uv11;

        //choosing pos on material based of start or end
        if (cell.start == true)
        {
            //corners of material
            uv00 = new Vector2(0, 0.52f);
            uv10 = new Vector2(0.49f, 0.52f);
            uv01 = new Vector2(0.49f, 1f);
            uv11 = new Vector2(0, 1f);
        }
        else if (cell.end == true)
        {
            //corners of material
            uv00 = new Vector2(0.52f, 0);
            uv10 = new Vector2(1f, 0);
            uv01 = new Vector2(1f, 0.48f);
            uv11 = new Vector2(0.52f, 0.48f);
        }
        else
        {
            //corners of material
            uv00 = new Vector2(0.01f, 0.01f);
            uv10 = new Vector2(0.48f, 0.01f);
            uv01 = new Vector2(0.48f, 0.48f);
            uv11 = new Vector2(0.01f, 0.48f);
        }

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

    public List<GameObject> getMeshes()
    {
        return objects;
    }
}

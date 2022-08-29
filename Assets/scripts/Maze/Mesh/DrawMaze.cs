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

    public GameObject[,] objects;

    private MazeGenerator generator;

    //constructor
    public DrawMaze(Grid _grid, Material _mat, GameObject _gameObject)
    {
        (this.width, this.height) = (_grid.width, _grid.height);

        this.empty = _gameObject;

        this.grid = _grid;
        this.mat = _mat;

        objects = new GameObject[CalculateAmount(width), CalculateAmount(height)];

        generator = MazeGenerator.getInstance();
    }

    //create all the triangles,verts and uv's
    public Vector2 DrawTerrain(int iv, int jv)
    {
        if (jv == CalculateAmount(height))
        {
            jv = 0;
            iv++;

        }
        if (iv == CalculateAmount(width))
        {
            iv = 0;
        }

#pragma warning disable CS0162 // Unreachable code detected
        for (int i = iv; i < CalculateAmount(width); i++)
        {
            for (int j = jv; j < CalculateAmount(height); j++)
            {
                GameObject obj;
                if (objects[i , j] != null)
                {
                    obj = objects[i , j];
                }
                else
                {
                    obj = MazeGenerator.Instantiate(empty, new Vector3(i * 10, 0, j * 10), new Quaternion(0, 0, 0, 0));
                    obj.name = $"{i},{j}";
                    objects[i, j] = obj;
                }


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
                        if(cell.Use == true)
                        {
                            quads quads = new quads(x, y);
                            Vector2[] uv = getUvs(cell);

                            //for 5 faces
                            for (int k = 0; k < 5; k++)
                            {
                                if (cell.walls[k] == true)
                                {
                                    Vector3[] v = quads.Face(k);
                                    //for 2 triangle's with 3 corners
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
                }
                //setting all the gathered intel on mesh
                mesh.vertices = vertices.ToArray();
                mesh.triangles = triangles.ToArray();
                mesh.uv = uvs.ToArray();
                mesh.RecalculateNormals();

                MeshFilter meshFilter;
                MeshRenderer meshRenderer;
                MeshCollider collider;
                if (obj.GetComponent<MeshRenderer>() == null)
                {
                    meshFilter = obj.AddComponent<MeshFilter>();
                    meshRenderer = obj.AddComponent<MeshRenderer>();
                    collider = obj.AddComponent<MeshCollider>();
                }
                else
                {
                    meshFilter = obj.GetComponent<MeshFilter>();
                    collider = obj.GetComponent<MeshCollider>();
                }
                if(mesh != collider.sharedMesh)
                {
                    meshFilter.mesh = mesh;
                    collider.sharedMesh = mesh;
                }
                DrawTexture(obj);
                break;
            }
            break;
        }
#pragma warning restore CS0162 // Unreachable code detected
        return new Vector2(iv,(jv + 1));
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
    private Vector2[] getUvs(Cell _cell)
    {
        Vector2 uv00;
        Vector2 uv10;
        Vector2 uv01;
        Vector2 uv11;

        //choosing pos on material based of start or end
        if (_cell.start == true)
        {
            //corners of material
            uv00 = new Vector2(0, 0.52f);
            uv10 = new Vector2(0.49f, 0.52f);
            uv01 = new Vector2(0.49f, 1f);
            uv11 = new Vector2(0, 1f);
        }
        else if (_cell.end == true)
        {
            //corners of material
            uv00 = new Vector2(0.52f, 0);
            uv10 = new Vector2(1f, 0);
            uv01 = new Vector2(1f, 0.48f);
            uv11 = new Vector2(0.52f, 0.48f);
        }
        else if(_cell.IsPortal == true)
        {
            //corners of material
            uv00 = new Vector2(0.52f, 0.52f);
            uv10 = new Vector2(0.99f, 0.52f);
            uv01 = new Vector2(0.99f, 0.99f);
            uv11 = new Vector2(0.52f, 0.99f);
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

    public GameObject[,] getMeshes()
    {
        return objects;
    }
}

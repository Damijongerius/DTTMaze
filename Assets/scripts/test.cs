using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Material mat;
    void Start()
    {
        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Draw()
    {

        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        Cell cell = new Cell(1,1);
        cell.walls[4] = false;

        quads quads = new quads(0, 0);
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
        //setting all the gathered intel on mesh
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        DrawTexture(gameObject);
    }

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

    void DrawTexture(GameObject obj)
    {
        //puting the material on all the verts
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}



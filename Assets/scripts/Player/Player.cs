using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Vector3 position;

    private Grid grid;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grid = Grid.GetInstance();
        foreach(Cell cell in grid.cells)
        {
            if(cell.start)
            {
                position = new Vector3(cell.x, 0, cell.y);
            }
        }
    }

    private void Update()
    {
        Move();
        Debug.Log("a");
    }

    public void Move()
    {
        //getting the input from 2 axis to see if its minus or plus
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //calculating the direction 
        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        dir = new Vector3(dir.x, 0, dir.z);

        //moving
        rb.AddForce(speed * Time.deltaTime * dir);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpecial();
        }
    }

    private void OnSpecial()
    {
        grid = Grid.GetInstance();
        Vector3 pos = transform.position;
        Cell cell = grid.cells[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)];
        if (cell.IsPortal)
        {
            transform.position = new Vector3(cell.ConnectPoint.x, 0, cell.ConnectPoint.y);
        }else if (cell.end)
        {

        }
    }
}

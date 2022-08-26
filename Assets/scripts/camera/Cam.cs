using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    //speeds
    public float speed;
    public float boost;

    //the actual input speed
    private float totalSpeed;

    //the visual camera
    private Camera cam;
    private void Awake()
    {
        //getting the component in the child
        cam = gameObject.GetComponentInChildren<Camera>();
    }
    void Update()
    {
        //shift for speed up
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalSpeed = boost + speed;
        }
        else
        {
            totalSpeed = speed;
        }
        //moving the camera
        Move();

        //rotation
            if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 0.1f, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -0.1f, 0, Space.Self);
        }

        Zoom();
    }

    //the movement method
    public void Move()
    {
        //getting the input from 2 axis to see if its minus or plus
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //calculating the direction 
        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        dir = new Vector3(dir.x, 0, dir.z);

        transform.position += totalSpeed * Time.deltaTime * dir;

    }

    //the rotation method
    public void Zoom()
    {
        //rotating with scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, cam.transform.position);

        cam.transform.position += scrollInput * 15 * cam.transform.forward;
    }

}

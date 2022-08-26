using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float speed;
    public float boost;

    private float totalSpeed;

    private Camera cam;
    private void Awake()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalSpeed = boost + speed;
        }
        else
        {
            totalSpeed = speed;
        }
        Move();

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

    public void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        dir = new Vector3(dir.x, 0, dir.z);

        transform.position += totalSpeed * Time.deltaTime * dir;

    }

    public void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, cam.transform.position);

        cam.transform.position += scrollInput * 15 * cam.transform.forward;
    }

}

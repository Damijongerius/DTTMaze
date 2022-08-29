using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //gameobjects containing the ui elements
    public GameObject button1;
    public GameObject button2;

    public GameObject widthfield;
    public GameObject heightfield;

    //ui elements
    private Button generate;
    private Button fullMazeView;

    private TMP_InputField widthInput;
    private TMP_InputField heightInput;

    //width height for centering camera
    private Vector2 widthHeight;
    public GameObject camera;
    private bool canDo = false;
    private Vector3 pos;

    void Start()
    {
        //linking the components to variables
        generate = button1.GetComponent<Button>();
        fullMazeView = button2.GetComponent<Button>();

        widthInput = widthfield.GetComponent<TMP_InputField>();
        heightInput = heightfield.GetComponent<TMP_InputField>();

        //adding an eventlistener to the buttons
        generate.onClick.AddListener(RunGenerator);
        fullMazeView.onClick.AddListener(SetCameraPos);
    }

    //giving data to mazegenerator
    private void RunGenerator()
    {
        Vector2 wH = ToInt();
        
        //testing variables if they are in between the right cords
        if(wH.x != 0)
        {
            MazeGenerator.getInstance().Generator((int)wH.x,(int)wH.y);
        }
        else
        {
            Debug.Log("the number you tried putting in is not between(10 and 250)");
        }


        Vector2 ToInt()
        {
            Vector2 wH = new Vector2();
            try
            {
                //converting string into int
                wH.x = int.Parse(heightInput.text);
                wH.y = int.Parse(widthInput.text);

                //testing if variables are in between right cords
                if (wH.x > 2000)
                {
                    return new Vector2(0,0);
                }
                if (wH.y > 2000)
                {
                    return new Vector2(0, 0);
                }
                if (wH.x < 10)
                {
                    return new Vector2(0, 0);
                }
                if (wH.y < 10)
                {
                    return new Vector2(0, 0);
                }
            }
            catch(Exception e)
            {
                //if its not possible it will say this
                Debug.Log(e);
                Debug.Log("you have to put numbers in the text fields");
            }
            widthHeight = wH;
            return wH;
        }
    }

    //method for centering cam pos
    private void SetCameraPos()
    {
        //it only contains if statment centering is done in update
        if(canDo == true)
        {
            canDo = false;
        }
        else
        {
            canDo = true;
        }
    }

    private void Update()
    {
        if (canDo == true)
        {
            pos = camera.transform.position;
            camera.transform.position = new Vector3(Mathf.Lerp(pos.x, widthHeight.x / 2, 0.001f), Mathf.Lerp(pos.y, widthHeight.x / 2 + widthHeight.y / 2, 0.001f), Mathf.Lerp(pos.z, widthHeight.y / 2, 0.001f));
        }
    }


}

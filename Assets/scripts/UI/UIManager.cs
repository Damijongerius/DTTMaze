using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;

    public GameObject widthfield;
    public GameObject heightfield;

    //ui elements
    private Button generate;
    private Button fullMazeView;

    private TMP_InputField widthInput;
    private TMP_InputField heightInput;

    private Vector2 widthHeight;

    public GameObject camera;

    private bool canDo = false;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        generate = button1.GetComponent<Button>();
        fullMazeView = button2.GetComponent<Button>();

        widthInput = widthfield.GetComponent<TMP_InputField>();
        heightInput = heightfield.GetComponent<TMP_InputField>();

        generate.onClick.AddListener(RunGenerator);
        fullMazeView.onClick.AddListener(SetCameraPos);
    }

    private void Update()
    {
        if(canDo == true)
        {
            pos = camera.transform.position;
            camera.transform.position = new Vector3(Mathf.Lerp(pos.x, widthHeight.x / 2, 0.0003f), Mathf.Lerp(pos.y, widthHeight.x + widthHeight.y / 3, 0.0003f), Mathf.Lerp(pos.z, widthHeight.y / 2, 0.0003f));
        }
    }


    private void RunGenerator()
    {
        Vector2 wH = ToInt();
        
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
                wH.x = int.Parse(heightInput.text);
                wH.y = int.Parse(widthInput.text);
                Debug.Log(wH);
                if (wH.x > 250)
                {
                    return new Vector2(0,0);
                }
                if (wH.y > 250)
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
                Debug.Log(e);
                Debug.Log("you have to put numbers in the text fields");
            }
            widthHeight = wH;
            return wH;
        }
    }

    private void SetCameraPos()
    {
        if(canDo == true)
        {
            canDo = false;
        }
        else
        {
            canDo = true;
        }
    }


}

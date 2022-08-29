using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //gameobjects containing the ui elements
    public GameObject gen;
    public GameObject view;

    public GameObject widthfield;
    public GameObject heightfield;

    public GameObject dropDownObj;

    public GameObject Playerview;

    //ui elements
    private Button generate;
    private Button fullMazeView;

    private TMP_InputField widthInput;
    private TMP_InputField heightInput;

    private TMP_Dropdown dropdown;

    private Button Playerv;

    //width height for centering camera
    private Vector2 widthHeight;
    public new GameObject camera;
    private bool canDo = false;
    private Vector3 pos;

    //player
    public GameObject prefab;
    private GameObject Player;
    public static bool pov;

    void Start()
    {
        //linking the components to variables
        generate = gen.GetComponent<Button>();
        fullMazeView = view.GetComponent<Button>();

        widthInput = widthfield.GetComponent<TMP_InputField>();
        heightInput = heightfield.GetComponent<TMP_InputField>();

        dropdown = dropDownObj.GetComponent<TMP_Dropdown>();

        Playerv = Playerview.GetComponent<Button>();

        //adding an eventlistener to the buttons
        generate.onClick.AddListener(RunGenerator);
        fullMazeView.onClick.AddListener(SetCameraPos);
        Playerv.onClick.AddListener(PlayerView);
    }

    //giving data to mazegenerator
    private void RunGenerator()
    {
        Vector2 wH = ToInt();
        
        //testing variables if they are in between the right cords
        if(wH.x != 0)
        {
            MazeGenerator.getInstance().Generator((int)wH.x,(int)wH.y, GetType());
            Destroy(Player);
        }
        else
        {
            Debug.Log("the number you tried putting in is not between(10 and 2000)");
            heightInput.text = "not Possible";
            widthInput.text = "not Possible";
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
            catch
            {
                //if its not possible it will say this
                Debug.Log("you have to put numbers in the text fields");
                heightInput.text = "not Possible";
                widthInput.text = "not Possible";
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
        pos = camera.transform.position;
        Vector3 playerPos;
        
        if (canDo)
        {     
            //lerping camera pos
            camera.transform.position = new Vector3(Mathf.Lerp(pos.x, widthHeight.x / 2, 0.001f), Mathf.Lerp(pos.y, widthHeight.x / 2 + widthHeight.y / 2, 0.001f), Mathf.Lerp(pos.z, widthHeight.y / 2, 0.001f));
        }else if (pov && Player != null)
        {
            playerPos = Player.transform.position;
            //lerping cam to playerpos
            camera.transform.position = new Vector3(Mathf.Lerp(pos.x, playerPos.x, 0.006f), pos.y, Mathf.Lerp(pos.z, playerPos.z, 0.006f));
        }
    }

    private new Type GetType()
    {
        if(dropdown.value == 0)
        {
            return Type.SQUARE;
        }else if(dropdown.value == 1)
        {
            return Type.PERLINNOISE;
        }
        return Type.SQUARE;
    }

    public void PlayerView()
    {
        if(Player == null) Player = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);

        if (pov) pov = false;
        else pov = true;
    }


}

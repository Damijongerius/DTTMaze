using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClose : MonoBehaviour
{
    public GameObject canvasOpen;
    public GameObject canvasClose;

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChangeState);
    }

    [System.Obsolete]
    public void ChangeState()
    {
        canvasOpen.active = true;
        canvasClose.active = false;
    }
}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PushButtonScript : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate { };

    private int dividerPosition;
    private string buttonName, buttonValue;
    // Start is called before the first frame update
    void Start()
    {
        buttonName = gameObject.name;
        dividerPosition = buttonName.IndexOf("_");
        buttonValue = buttonName.Substring(0, dividerPosition);

        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);

    }

    private void ButtonClicked(){
        ButtonPressed(buttonValue);
    }
}

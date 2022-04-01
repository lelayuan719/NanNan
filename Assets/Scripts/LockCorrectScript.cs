using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCorrectScript : MonoBehaviour
{
    public bool isSolved;
    private string codeSequence;
    private int codeLength;
    // Start is called before the first frame update
    void Start()
    {
        isSolved = false;
        codeSequence = "";
        codeLength = 7;
        PushButtonScript.ButtonPressed += AddDigitToSequence;
    }

    private void AddDigitToSequence(string digit){
        if (codeSequence.Length < codeLength){
            switch (digit){
                case "One":
                    codeSequence += "1";
                    Debug.Log("1");
                    break;
                case "Two":
                    codeSequence += "2";
                    Debug.Log("2");
                    break;
                case "Three":
                    codeSequence += "3";
                    Debug.Log("3");
                    break;
                case "Four":
                    codeSequence += "4";
                    Debug.Log("4");
                    break;
                case "Five":
                    codeSequence += "5";
                    Debug.Log("5");
                    break;
                case "Six":
                    codeSequence += "6";
                    Debug.Log("6");
                    break;
                case "Seven":
                    codeSequence += "7";
                    Debug.Log("7");
                    break;
                case "Eight":
                    codeSequence += "8";
                    Debug.Log("8");
                    break;
                case "Nine":
                    codeSequence += "9";
                    Debug.Log("9");
                    break;
            }
        }
        switch (digit){
            case "Submit":
                CheckResult();
                break;
        }
    }

    private void CheckResult(){
        Debug.Log("Checking result");
        if (codeSequence == "1964618"){
            Debug.Log("Correct!");
            isSolved = true;
        }
        else{
            Debug.Log("Incorrect!");
            ClearCode();
        }
    }

    private void ClearCode(){
        Debug.Log("Cleared");
        codeSequence = "";
    }

    /*private void OnDestroy(){
        PushButtonScript.ButtonPressed -= AddDigitToSequence;
    }*/
}

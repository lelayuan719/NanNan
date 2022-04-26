using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockCorrectScript : MonoBehaviour
{
    public bool isSolved;
    private string codeSequence;
    private int codeLength;
    [SerializeField]
    private Sprite[] digits;
    [SerializeField]
    private Image[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        isSolved = false;
        codeSequence = "";
        codeLength = 7;
        PushButtonScript.ButtonPressed += AddDigitToSequence;
        for (int i = 0; i < numbers.Length; i++){
            numbers[i].sprite = digits[9];
        }
    }

    private void AddDigitToSequence(string digit){
        if (codeSequence.Length < codeLength){
            switch (digit){
                case "One":
                    codeSequence += "1";
                    Display(1);
                    break;
                case "Two":
                    codeSequence += "2";
                    Display(2);
                    break;
                case "Three":
                    codeSequence += "3";
                    Display(3);
                    break;
                case "Four":
                    codeSequence += "4";
                    Display(4);
                    break;
                case "Five":
                    codeSequence += "5";
                    Display(5);
                    break;
                case "Six":
                    codeSequence += "6";
                    Display(6);
                    break;
                case "Seven":
                    codeSequence += "7";
                    Display(7);
                    break;
                case "Eight":
                    codeSequence += "8";
                    Display(8);
                    break;
                case "Nine":
                    codeSequence += "9"; 
                    Display(9);
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
        else {
            Debug.Log("Incorrect!");
            ClearCode();
        }
    }

    private void ClearCode(){
        Debug.Log("Cleared");
        for (int i = 0; i < numbers.Length; i++){
            numbers[i].sprite = digits[9];
        }
        codeSequence = "";
    }

    private void Display(int recentDigit){
        numbers[codeSequence.Length-1].sprite = digits[recentDigit-1];
    }

    /*private void OnDestroy(){
        PushButtonScript.ButtonPressed -= AddDigitToSequence;
    }*/
}

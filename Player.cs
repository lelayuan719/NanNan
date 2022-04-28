using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    //Initialize 2 of 3 variables
    public int level = 0;
    public int health = 50;

    //Save: call SavePlayer in SaveSystem.cs 
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    //Load: call LoadPlayer in SaveSystem.cs and update variables
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        transform.position = position;
    }


    #region UI Methods

    //Can be ignored
    public void ChangeLevel (int amount)
    {
        level += amount;
    }

    public void ChangeHealth (int amount)
    {
        health += amount;
    }

    #endregion
}


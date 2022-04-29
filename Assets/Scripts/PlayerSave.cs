using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSave : MonoBehaviour {

    //Initialize 2 of 3 variables
    // I think this is where all player data should be stored. Other files that want to access these data should refrence this file 
    // this file is the one that updates the player information -- only this one!
    //change these variables to refer to specific environmental elements
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


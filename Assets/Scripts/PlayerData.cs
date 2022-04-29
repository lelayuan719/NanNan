using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    //Example player has 3 variables, should be replaced by variables of Nannan
    public int level;
    public int health;
    public float[] position;

    public PlayerData (PlayerSave player)
    {
        //Initialize variables
        level = player.level;
        health = player.health;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }
}

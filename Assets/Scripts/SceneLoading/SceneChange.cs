using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] protected string NextScene;

    public virtual void ChangeScene()
    {
        GameManager.GM.LoadScene(NextScene);
    }
}

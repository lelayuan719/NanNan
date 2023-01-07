using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekReset : MonoBehaviour
{
    [SerializeField] Transform principalReset;
    [SerializeField] Transform playerReset;
    [SerializeField] CinemachineVirtualCamera resetCam;

    public void ResetScene()
    {
        // Principal
        transform.parent.position = principalReset.position;
        GetComponent<PrincipalHideAndSeek>().ResetState();

        // Player
        GameManager.GM.player.transform.position = playerReset.position;
        GameManager.GM.player.gameObject.GetComponent<NoteFragmentHandler>().ResetNotes();

        // Camera
        GameManager.GM.ChangeActiveCam(resetCam.gameObject);

        // Scene
        GetComponent<SimpleTransitionSameScene>().Transition();
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class journalPageOpener : MonoBehaviour {

//     public GameObject amuletButton;
//     public GameObject shardButton;
//     public GameObject bookButton;
//     public GameObject amuletPanel;
//     public GameObject shardPanel;
//     public GameObject bookPanel;


//     public void Start(){
//         amuletPanel.SetActive(false);
//         shardPanel.SetActive(false);
//         bookPanel.SetActive(false);
//     }

//     void Update(){
//         amuletButton.onClick.getcaddListener(openAmuletPanel());
//         shardButton.onClick.addListener(openShardPanel());
//         bookButton.onClick.addListener(openBookPanel());
//     }

//     public void openAmuletPanel()
//     {
//         if(shardPanel.activeSelf){
//             shardPanel.SetActive(false);
//         } else if(bookPanel.activeSelf){
//             bookPanel.SetActive(false);
//         }

//         amuletPanel.SetActive(true);

//     }

//     public void openShardPanel()
//     {
//         if(amuletPanel.activeSelf){
//             amuletPanel.SetActive(false);
//         } else if(bookPanel.activeSelf){
//             bookPanel.SetActive(false);
//         }

//         shardPanel.SetActive(true);

//     }

//     public void openBookPanel()
//     {
//         if(shardPanel.activeSelf){
//             shardPanel.SetActive(false);
//         } else if(amuletPanel.activeSelf){
//             amuletPanel.SetActive(false);
//         }

//         bookPanel.SetActive(true);

//     }
   
// }

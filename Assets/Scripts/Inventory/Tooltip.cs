using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltip;
    public Text tooltipText;

    void Start(){
        tooltip = GetComponentInChildren<Text>();
        tooltip.gameObject.SetActive(true);
    }

    public void GenerateTooltip(Item item){
        string tooltip = string.Format("<b>{0}</b>\n{1}",item.title, item.description);
        gameObject.SetActive(true);
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = tooltip;
    }
}

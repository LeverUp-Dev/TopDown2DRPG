using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    [HideInInspector]
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction) {//exit
            isAction = false;
        }
        else {//enter
            isAction = true;
            scanObject = scanObj;
            talkText.text = $"����� {scanObject.name}�� �� ����.";
        }
        talkPanel.SetActive(isAction);
    }
}

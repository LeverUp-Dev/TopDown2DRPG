using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager; //퀘스트매니저 변수 생성
    public GameObject talkPanel;
    public Image portrait;
    public Text talkText;
    [HideInInspector]
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    public void Action(GameObject scanObj)
    {
        //Get current obj;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        //visible talk for action
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        //set talk data
        int questTalkIndex = questManager.GetQuestTalkIndex(id); //퀘스트 번호 가져오기
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        //end talk
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        //continue talk
        if (isNPC) {
            talkText.text = talkData.Split(':')[0];

            portrait.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portrait.color = new Color(1, 1, 1, 1);
        }
        else {
            talkText.text = talkData;

            portrait.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}

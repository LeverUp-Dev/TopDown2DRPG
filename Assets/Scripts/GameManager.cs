using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager; //퀘스트매니저 변수 생성
    public Animator talkPanel;
    public Animator portraitAnim;
    public Image portrait;
    [HideInInspector]
    public Sprite prevPortrait;
    public TypeEffect talkText;
    public Text NPCName;
    public Text questTxt;
    public GameObject menuSet;
    [HideInInspector]
    public GameObject scanObject;
    public GameObject player;
    public bool isAction;
    public int talkIndex;

    void Start()
    {
        GameLoad();
        questTxt.text = questManager.CheckQuest();
    }

    void Update()
    {
        //sub menu
        if (Input.GetButtonDown("Cancel")) {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }

    public void Action(GameObject scanObj)
    {
        //Get current obj;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        //visible talk for action
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC)
    {
        //set talk data
        int questTalkIndex;
        string talkData;

        if (talkText.isAnim) {
            talkText.SetMsg("");
            return;
        }
        else {
            questTalkIndex = questManager.GetQuestTalkIndex(id); //퀘스트 번호 가져오기
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        //end talk
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            questTxt.text = questManager.CheckQuest(id);
            return;
        }
        //continue talk
        if (isNPC) {
            NPCName.color = new Color(0, 1, 0, 1);
            NPCName.text = scanObject.GetComponentInChildren<ObjectData>().NPCName;
            talkText.SetMsg(talkData.Split(':')[0]);
            //show portrait
            portrait.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portrait.color = new Color(1, 1, 1, 1);
            //anim portrait 
            if (prevPortrait != portrait.sprite) {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portrait.sprite;
            }
        }
        else {
            NPCName.color = new Color(0, 1, 0, 0);
            talkText.SetMsg(talkData);
            //hide portrait
            portrait.color = new Color(1, 1, 1, 0);
        }
        //next talk
        isAction = true;
        talkIndex++;
    }

    public void GameSave()
    {
        //player.x, player.y
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //quest ID
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        //quest action index
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObj();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화순서 변수
    public GameObject[] questObj;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화해 보자.", new int[] { 1000, 2000 })); //퀘스트 1
        questList.Add(20, new QuestData("루도의 동전을 찾아주기.", new int[] { 5000, 2000 })); //퀘스트 2
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 })); //퀘스트 3
    }

    public int GetQuestTalkIndex(int id) //npc ID를 받고 퀘스트번호를 반환하는 함수
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //next talk target
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
        //control quest obj
        ControlObj();
        //talk complete & next quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();
        //quest name
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //quest name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObj()
    {
        switch (questId) {
            case 10:
                if (questActionIndex == 2)
                    questObj[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObj[0].SetActive(true);
                if (questActionIndex == 1)
                    questObj[0].SetActive(false);
                break;
        }
    }
}
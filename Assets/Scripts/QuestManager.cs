using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //����Ʈ ��ȭ���� ����
    public GameObject[] questObj;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� ������ ��ȭ�� ����.", new int[] { 1000, 2000 })); //����Ʈ 1
        questList.Add(20, new QuestData("�絵�� ������ ã���ֱ�.", new int[] { 5000, 2000 })); //����Ʈ 2
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[] { 0 })); //����Ʈ 3
    }

    public int GetQuestTalkIndex(int id) //npc ID�� �ް� ����Ʈ��ȣ�� ��ȯ�ϴ� �Լ�
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
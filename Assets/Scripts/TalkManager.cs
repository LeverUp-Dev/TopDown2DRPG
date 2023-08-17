using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();//�� ��ü�� ���� ������ �Ҵ��� ���̱⿡ ���ڿ� �迭�� ���
        portraitData = new Dictionary<int, Sprite>();//NPC�� ������ ���� �ʻ�ȭ�� �����ϱ� ���� ��������Ʈ ���
        GenerateData();
    }

    void GenerateData()
    {
        //NPC
        talkData.Add(1000, new string[]
        {
            "�ȳ�?:0",
            "�� ���� ó�� �Ա���?:1"
        });
        talkData.Add(2000, new string[]
        {
            "? ��? �������� �������̱�.:1",
            "�� ȣ�� �Ƹ����� �ʳ�?:0",
            "�������� ���� �������� ������ ���ϸ� �� ȣ���� ����� ������ �ִٰ� �ϴ���.:3"
        });
        //Obj
        talkData.Add(100, new string[] { "����� �������ڴ�." });
        talkData.Add(200, new string[] { "������ ����ߴ� ������ �ִ� å���̴�." });
        //quest talk
        talkData.Add(10 + 1000, new string[] {
            "� ��.:0",
            "�� ������ ���� ������ �ִٴ� �� �˰��ִ�?:1",
            "������ ȣ�� �ʿ� �絵�� �˷��ٲ���.:0" });
        talkData.Add(11 + 2000, new string[] {
            "? ��? �������� �������̱�.:1",
            "�� ������ ������ ������ �� �ǰ�?:0",
            "�׷��� �ϴ� ��Ź�� �ִµ�...:1",
            "�� �� ��ó�� ������ ���� �� ã�������� ��.:0" });

        talkData.Add(20 + 1000, new string[] {
            "�絵�� ����?.:1",
            "�絵�� ������ �Ҿ���ȳ� ���� ���߿� �Ѹ��� �ؾ߰ھ�:3" });
        talkData.Add(20 + 2000, new string[] { "���߿� ã���� �� �� ������ ��:1" });
        talkData.Add(20 + 5000, new string[] { "�絵�� ������ ã�� �� ����." });
        talkData.Add(21 + 2000, new string[] { "��!, ã���༭ ������ ����!.:1" });
        //NPC Luna
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        //NPC Ludo
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id)) {
            if(!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); //get first talk
            else
                return GetTalk(id - id % 10, talkIndex); //get first quest talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
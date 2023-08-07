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
        talkData = new Dictionary<int, string[]>();//한 객체에 여러 문장을 할당할 것이기에 문자열 배열을 사용
        portraitData = new Dictionary<int, Sprite>();//NPC의 감정에 따라 초상화를 변경하기 위한 스프라이트 사용
        GenerateData();
    }

    void GenerateData()
    {
        //NPC
        talkData.Add(1000, new string[]
        { 
            "안녕?:0",
            "이 곳에 처음 왔구나?:1"
        });
        talkData.Add(2000, new string[]
        {
            "? 아? 외지인은 오랜만이군.:5",
            "이 호수 아름답지 않나?:4",
            "마을에서 대대로 내려오는 전설에 의하면 이 호수에 비밀이 숨져겨 있다고 하더군.:6"
        });
        //Obj
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });

        //NPC Luna
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        //NPC Ludo
        portraitData.Add(2000 + 4, portraitArr[4]);
        portraitData.Add(2000 + 5, portraitArr[5]);
        portraitData.Add(2000 + 6, portraitArr[6]);
        portraitData.Add(2000 + 7, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
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

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
            "? 아? 외지인은 오랜만이군.:1",
            "이 호수 아름답지 않나?:0",
            "마을에서 대대로 내려오는 전설에 의하면 이 호수에 비밀이 숨져겨 있다고 하더군.:3"
        });
        //Obj
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });
        //quest talk
        talkData.Add(10 + 1000, new string[] {
            "어서 와.:0",
            "이 마을에 놀라운 전설이 있다는 거 알고있니?:1",
            "오른쪽 호수 쪽에 루도가 알려줄꺼야.:0" });
        talkData.Add(11 + 2000, new string[] {
            "? 아? 외지인은 오랜만이군.:1",
            "이 마을의 전설을 들으러 온 건가?:0",
            "그러면 일단 부탁이 있는데...:1",
            "내 집 근처에 떨어진 동전 좀 찾아줬으면 해.:0" });

        talkData.Add(20 + 1000, new string[] {
            "루도의 동전?.:1",
            "루도가 동전을 잃어버렸나 보네 나중에 한마디 해야겠어:3" });
        talkData.Add(20 + 2000, new string[] { "나중에 찾으면 꼭 좀 가져다 줘:1" });
        talkData.Add(20 + 5000, new string[] { "루도의 동전을 찾은 것 같다." });
        talkData.Add(21 + 2000, new string[] { "오!, 찾아줘서 정말로 고맙군!.:1" });
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
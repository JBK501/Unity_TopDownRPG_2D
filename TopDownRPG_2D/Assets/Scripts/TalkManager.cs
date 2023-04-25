using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; // 대화 데이터 저장
    Dictionary<int, Sprite> portraitData; // 초상화 데이터 저장

    public Sprite[] portraitArray;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();  
        GenerateData();
    }

    // 대화와 관련된 데이터를 생성하는 함수
    void GenerateData()
    {
        // 대화 내용을 추가한다.
        // NPC 표정은 문장과 1:1 매칭한다.
        // NPC A : 1000, NPC B : 2000
        // Box : 100, Desk : 200
        talkData.Add(1000, new string[] { "여어.:1", 
                                          "이 호수는 정말 아름답지?:0",
                                          "사실 이 호수에는 무언가 비밀이 숨겨져 있다고 해.:1" });
        talkData.Add(2000, new string[] { "안녕.:0", 
                                          "이곳에 처음 왔구나?:1",
                                          "한번 둘러보도록 해.:0" });
        talkData.Add(3000, new string[] {"평범한 나무 상자다."});
        talkData.Add(4000, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });


        // 퀘스트 대화내용을 추가한다.
        talkData.Add(10 + 2000, new string[] {"어서와.:0",
                                              "이 마을에 놀라운 전설이 있다는데:1",
                                              "오른쪽 호수 쪽에 루도가 알려줄꺼야.:0"});

        talkData.Add(11 + 2000, new string[] { "아직 못 만났어?:0",
                                               "루도는 오른쪽 호수 쪽에 있어.:0"});
        talkData.Add(11 + 1000, new string[] {"여어.:1",
                                              "이 호수의 전설을 들으러 온거야?:0",
                                              "그럼 일 좀 하나 해주면 좋을텐데....:1",
                                              "내 집 근처에 떨어진 동전 좀 주워줬으면 해.:0"});

        talkData.Add(20 + 2000, new string[] {"루도의 동전?:1",
                                              "돈을 흘리고 다니면 못쓰지!:3",
                                              "나중에 루도에게 한마디 해야겠어.:3"});

        talkData.Add(20 + 1000, new string[] { "찾으면 꼭 좀 가져다 줘.:1" });
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });

        talkData.Add(21 + 1000, new string[] { "엇, 찾아줘서 고마워.:2" });
        

        // NPC별로 표정을 추가한다.
        // 0 : Normal, 1 : Speak, 2 : Happy, 3 : Angry 
        portraitData.Add(2000 + 0, portraitArray[0]);
        portraitData.Add(2000 + 1, portraitArray[1]);
        portraitData.Add(2000 + 2, portraitArray[2]);
        portraitData.Add(2000 + 3, portraitArray[3]);
        portraitData.Add(1000 + 0, portraitArray[4]);
        portraitData.Add(1000 + 1, portraitArray[5]);
        portraitData.Add(1000 + 2, portraitArray[6]);
        portraitData.Add(1000 + 3, portraitArray[7]);
    }

    // 대화 내용을 반환하는 함수.
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10)) // 퀘스트 기본대사가 없을 때
                return GetTalk(id - id % 100, talkIndex); // 기본대사를 가져온다.
            else
                return GetTalk(id - id % 10, talkIndex);    // 퀘스트 기본대사를 가져온다.
          
            #region [이전로직]
            //if(!talkData.ContainsKey(id - id % 10))
            //{
            //    // 퀘스트 맨 처음 대사마저 없을 때
            //    // 기본 대사를 가지고 온다.
            //    if (talkIndex == talkData[id - id % 100].Length)
            //        return null;
            //    else
            //        return talkData[id - id % 100][talkIndex];
            //}
            //else
            //{
            //    // 해당 퀘스트 진행 순서 대사가 없을 때
            //    // 퀘스트 맨 처음 대사를 가져온다.
            //    if (talkIndex == talkData[id - id % 10].Length)
            //        return null;
            //    else
            //        return talkData[id - id % 10][talkIndex];
            //}
            #endregion
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    // NPC표정을 반환하는 함수.
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}

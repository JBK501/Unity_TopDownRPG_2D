using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    
    // 퀘스트를 생성하는 함수
    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기", new int[] { 2000, 1000 }));

        questList.Add(20, new QuestData("루도의 동전 찾아주기.", new int[] { 5000, 1000 }));

        questList.Add(30, new QuestData("퀘스트 올 클리어!.", new int[] { 0 }));
    }

    // 퀘스트 번호를 추출하는 함수
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex; // 퀘스트 번호 + 퀘스트 대화순서
    }

    public string CheckQuest(int id)
    {
        // 해당 퀘스트에서 다음 순서로 이동한다.
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // 퀘스트 오브젝트를 조절한다.
        ControlObject();

        // 해당 퀘스트가 끝나면 다음 퀘스트로 이동한다.
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // 퀘스트 명칭을 반환한다.
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    // 다음 퀘스트를 위한 함수
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    // 퀘스트와 관련된 오브젝트를 관리하는 함수
    public void ControlObject()
    {
        // 퀘스트 번호와 대화 순서에 따라 오브젝트를 상태를 조절한다.
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if(questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
            default:
                    break;
        }
    }
}

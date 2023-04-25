using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;

    public GameObject talkPanel;
    public Image portraitImg;
    public Text talkText;

    public GameObject scanObject;

    public bool isAction;
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        // 스캔한 물체 정보를 가져온다.
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>(); // ObjectData에 물체의 id와 NPC여부가 저장되어 있음.

        // 대화 내용을 표시한다.
        Talk(objData.id, objData.isNPC);

        // 대화창을 활성화 한다.
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNPC)
    {
        // 대화 내용을 가져온다.
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);   
        
        // 대화가 끝났을 때
        if(talkData == null)    
        {
            isAction = false;   
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        // 텍스트를 표시한다.
        // NPC일 때만 이미지를 표시한다.
        if(isNPC)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1f, 1f, 1f, 0f);
        }

        isAction = true;
        talkIndex++;    
    }

}

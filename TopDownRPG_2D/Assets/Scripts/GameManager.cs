using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;

    public Animator talkPanel;
    public Animator portraitAnim;

    public Image portraitImg;
    public TypeEffect talk;
    public Text questText;

    public Sprite prevPortrait;
    public GameObject menuSet;
    public GameObject scanObject;
    public GameObject player;
    public bool isAction;
    public int talkIndex;

    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        // 서브 메뉴
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }
    }

    public void Action(GameObject scanObj)
    {
        // 스캔한 물체 정보를 가져온다.
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>(); // ObjectData에 물체의 id와 NPC여부가 저장되어 있음.

        // 대화 내용을 표시한다.
        Talk(objData.id, objData.isNPC);

        // 대화창을 활성화 한다.
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNPC)
    {
        int questTalkIndex = 0;
        string talkData = "";

        // 대화 내용을 가져온다.
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        // 대화가 끝났을 때
        if(talkData == null)    
        {
            isAction = false;   
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return;
        }

        // 텍스트를 표시한다.
        // NPC일 때만 이미지를 표시한다.
        if(isNPC)
        {
            // 대화를 표시한다.
            talk.SetMsg(talkData.Split(':')[0]);

            // 이미지를 표시한다.
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1f, 1f, 1f, 1f);
            
            // 이미지 애니메이션을 실행한다.
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            // 대화를 표시한다.
            talk.SetMsg(talkData);

            portraitImg.color = new Color(1f, 1f, 1f, 0f);
        }

        isAction = true;
        talkIndex++;    
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY",player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
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

        player.transform.position = new Vector3(x, y, 0f); 
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }

}

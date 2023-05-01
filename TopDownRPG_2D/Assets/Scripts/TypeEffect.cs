using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public GameObject EndCursor;
    
    public int CharPerSeconds; // 글자 재생 속도
    public bool isAnim;

    Text msgText;
    AudioSource audioSource;

    string targetMsg; // 표시할 대화 문자열
    int index;
    float interval;
    

    void Awake()
    {
        msgText = GetComponent<Text>(); 
        audioSource = GetComponent<AudioSource>();
    }


    // 대화 문자열을 받는 함수
    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    // 시작
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        
        isAnim = true;

        Invoke(nameof(Effecting), interval);
    }

    // 재생
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        Invoke(nameof(Effecting), interval);
    }

    // 종료
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}

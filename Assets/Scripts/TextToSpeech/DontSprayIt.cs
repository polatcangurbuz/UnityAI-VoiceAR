using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class DontSprayIt : MonoBehaviour
{
    SpVoice voice = new SpVoice();
    SpeechRecognition SpeechRecognition;
    [SerializeField] GameObject speechobject;
    private void Start()
    {
        SpeechRecognition = speechobject.GetComponent<SpeechRecognition>();
    }

    private void Update()
    {
        if (SpeechRecognition.promptbool)
        {
           
            StartCoroutine(waiting());
           
        }
    }

    IEnumerator waiting()
    {
        Debug.Log("girdi");
        yield return new WaitForSeconds(3f);
        voice.Speak(APIManager.Instance.response, SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
    }

   
}

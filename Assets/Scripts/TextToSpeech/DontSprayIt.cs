using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class DontSprayIt : MonoBehaviour
{
    SpVoice voice = new SpVoice();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            voice.Speak("Hello, what is your name?", SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
        }
    }
}

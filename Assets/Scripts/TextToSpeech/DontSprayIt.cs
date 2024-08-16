using System.Collections;
using UnityEngine;
using SpeechLib;

public class DontSprayIt : MonoBehaviour
{
    SpVoice voice = new SpVoice();
    SpeechRecognition SpeechRecognition;
    [SerializeField] GameObject speechobject;
    [SerializeField] AudioSource audioSource;
    AudioClip audioClip;
    string filePath = "C:\\VoiceAR\\Assets\\Sounds\\voicespeech.wav";
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
        yield return new WaitForSeconds(3f);

        // Sesin kaydedileceði dosya yolu
       

        // SpFileStream nesnesi oluþturma ve dosya yolu belirleme
        SpFileStream fileStream = new SpFileStream();
        fileStream.Open(filePath, SpeechStreamFileMode.SSFMCreateForWrite, false);

        // SPVoice'nin çýkýþýný dosyaya yönlendirme
        voice.AudioOutputStream = fileStream;
     
        // Metni sese dönüþtürme ve ayný anda sesli okuma
        voice.Speak(APIManager.Instance.response, SpeechVoiceSpeakFlags.SVSFDefault);

        // Dosyayý kapatma
        fileStream.Close();

        // Voice çýkýþýný varsayýlan aygýta geri döndürme
        voice.AudioOutputStream = null;

        // Sesi varsayýlan aygýttan oynatma 
        StartCoroutine(LoadAndPlayAudio());
    }
    IEnumerator LoadAndPlayAudio()
    {
        // Ses dosyasýný yeniden yükle
        using (var www = new WWW("file:///" + filePath))
        {
            yield return www;

            audioSource.clip = www.GetAudioClip();
            if(audioSource.clip.length >0) {
                audioSource.Play();
            }// Sesin çalýnmasýný baþlat
        }
    }

}

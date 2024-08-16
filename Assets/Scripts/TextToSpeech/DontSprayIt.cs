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

        // Sesin kaydedilece�i dosya yolu
       

        // SpFileStream nesnesi olu�turma ve dosya yolu belirleme
        SpFileStream fileStream = new SpFileStream();
        fileStream.Open(filePath, SpeechStreamFileMode.SSFMCreateForWrite, false);

        // SPVoice'nin ��k���n� dosyaya y�nlendirme
        voice.AudioOutputStream = fileStream;
     
        // Metni sese d�n��t�rme ve ayn� anda sesli okuma
        voice.Speak(APIManager.Instance.response, SpeechVoiceSpeakFlags.SVSFDefault);

        // Dosyay� kapatma
        fileStream.Close();

        // Voice ��k���n� varsay�lan ayg�ta geri d�nd�rme
        voice.AudioOutputStream = null;

        // Sesi varsay�lan ayg�ttan oynatma 
        StartCoroutine(LoadAndPlayAudio());
    }
    IEnumerator LoadAndPlayAudio()
    {
        // Ses dosyas�n� yeniden y�kle
        using (var www = new WWW("file:///" + filePath))
        {
            yield return www;

            audioSource.clip = www.GetAudioClip();
            if(audioSource.clip.length >0) {
                audioSource.Play();
            }// Sesin �al�nmas�n� ba�lat
        }
    }

}

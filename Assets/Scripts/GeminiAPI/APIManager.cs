using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIManager : MonoBehaviour
{
    [SerializeField] private string gasURL;
    public string prompt=" ";
    public string response,queryresp=" ";
    [SerializeField]TextMeshProUGUI tmp,responsetmp;
    SpeechRecognition speech;
    [SerializeField] GameObject speechrec;
    public static APIManager Instance { get; private set; }
    bool secondbutton = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
       speech =  speechrec.GetComponent<SpeechRecognition>();
       
    }

    private void Update()
    {
        //if (speech.promptbool)
        //{
        //    StartCoroutine(SendDataToGAS());

        //}
        if (secondbutton)
        {
            StartCoroutine(SendDataToGAS());
            secondbutton = false;
        }
    }

    private IEnumerator SendDataToGAS()
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", prompt);
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);

        yield return www.SendWebRequest();
        response = "";

        if (www.result == UnityWebRequest.Result.Success)
        {
            response = www.downloadHandler.text;
        }
        else
        {
            response = "Hata";
        }

        Debug.Log("response : "+response);
        responsetmp.text = response;
        
    }

   
    public void promptInput(string value)
    {
        prompt = value;
    }

    public void secondButton()
    {
        secondbutton = true;
    }

   
}
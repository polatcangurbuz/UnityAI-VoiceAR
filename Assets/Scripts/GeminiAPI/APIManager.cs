using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIManager : MonoBehaviour
{
    [SerializeField] private string gasURL;
    public string prompt;
    string response;
    [SerializeField]TextMeshProUGUI tmp;
    bool controlButton = false;

    public static APIManager Instance { get; private set; }

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


    private void Update()
    {
        if (controlButton)
        {
            StartCoroutine(SendDataToGAS());
            controlButton = false;
        }
    }

    private IEnumerator SendDataToGAS()
    {
        WWWForm form = new WWWForm();
        prompt = tmp.text;
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

        Debug.Log(response);


    }

   
    public void promptInput(string value)
    {
        prompt = value;
    }

    public void butonControl()
    {
        controlButton = true;
    }

   
}
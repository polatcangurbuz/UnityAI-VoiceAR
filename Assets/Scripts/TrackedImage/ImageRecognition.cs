using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    ARTrackedImageManager _artracked;
    [SerializeField] TextMeshProUGUI _TextMeshProUGUI;
    private void Awake()
    {
        _artracked = FindObjectOfType<ARTrackedImageManager>();
        if(_artracked ==null )
        {
            Debug.LogError("bulunamadý");
        }
    }

    private void OnEnable()
    {
        if(_artracked != null)
        {
            _artracked.trackedImagesChanged += OnImageChanged;
        }
    }

    private void OnDisable()
    {
        if( _artracked != null )
        {
            _artracked.trackedImagesChanged -= OnImageChanged;
        }
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var trackedImage in args.added)
        {
            _TextMeshProUGUI.text =  trackedImage.name;
        }
    }



}

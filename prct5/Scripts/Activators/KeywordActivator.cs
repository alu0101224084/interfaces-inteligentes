using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeywordActivator : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
      //GameManager.manager.recordMic();
      GameManager.manager.startRecognizeKeyword();
    }

    void OnTriggerExit(Collider other)
    {
      //GameManager.manager.recordMic();
      GameManager.manager.stopRecognizeKeyword();
    }

    public void startRecording() 
    {
       GameManager.manager.recordMic();
    }
}
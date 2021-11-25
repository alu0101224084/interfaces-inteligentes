using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void startRecordingMicDelegate();
    public static event startRecordingMicDelegate eventRecordMic;

    public delegate void playMicDelegate();
    public static event startRecordingMicDelegate eventPlayMic;

    public delegate void stopRecordingMicDelegate();
    public static event startRecordingMicDelegate eventStopRecordingMic;

    public delegate void recognizeKeywordEnterDelegate();
    public static event recognizeKeywordEnterDelegate eventRecognizeKeywordEnter;

    public delegate void recognizeKeywordExitDelegate();
    public static event recognizeKeywordExitDelegate eventRecognizeKeywordExit;

    public delegate void dictationEnterDelegate();
    public static event dictationEnterDelegate eventDictationEnter;

    public delegate void dictationExitDelegate();
    public static event dictationEnterDelegate eventDictationExit;

    public static GameManager manager;
    void Awake() 
      {
          if (manager == null)
          {
            manager = this;
            DontDestroyOnLoad(this);
          } else if (manager != this)
          {
              Destroy(gameObject);
          }
      }

    void Update()
    {
        
    }

    public void recordMic()
    {
        eventRecordMic();
    }

    public void playMic()
    {
        eventPlayMic();
    }

    public void stopRecordingMic()
    {
        eventStopRecordingMic();
    }

    public void startRecognizeKeyword()
    {
        eventRecognizeKeywordEnter();
    }

    public void stopRecognizeKeyword()
    {
        eventRecognizeKeywordExit();
    }

    public void DictationEnter()
    {
        eventDictationEnter();
    }

    public void DictationExit()
    {
        eventDictationExit();
    }
}

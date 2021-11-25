using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictationActivator : MonoBehaviour
{
    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
      GameManager.manager.DictationEnter();
    }

    void OnTriggerExit(Collider other)
    {
      GameManager.manager.DictationExit();
    }
}

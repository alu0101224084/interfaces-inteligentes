using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneControl : MonoBehaviour
{
    public delegate void MethodDelegateBigVolumeWithCamera();
    public static event MethodDelegateBigVolumeWithCamera eventBigVolumeWithCamera;
    Microphone microphone = new Microphone();
    AudioSource source;
    string[] devices;
    private float clipLoudness;
    private float[] clipSampleData;

    void Awake()
    {      
        devices = Microphone.devices;
        source = GetComponent<AudioSource>();
        clipSampleData = new float[1024];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            source.clip = Microphone.Start(devices[0], false, 1, 44100);
            
        }
        if (Input.GetKey(KeyCode.P))
        {
            source.Play();
            source.clip.GetData(clipSampleData, source.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= 1024;
            Debug.Log(clipLoudness);
            if (clipLoudness > 0.3f)
            {
                eventBigVolumeWithCamera();
            }
        }
    }
}

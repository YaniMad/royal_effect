using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BPMSync : MonoBehaviour
{
    [SerializeField] int bpm;
    [SerializeField] int signature;

    [SerializeField] AudioSource referenceAudio;

    [SerializeField] UnityEvent softBeatEvent;
    [SerializeField] UnityEvent strongBeatEvent;


    float nextBPMTime = 0f;
    int currentBPMCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (referenceAudio.time > nextBPMTime)
        {
            nextBPMTime += 60f / bpm;
            currentBPMCount++;

            softBeatEvent.Invoke();

            if (currentBPMCount%signature == 0)
            {
                strongBeatEvent.Invoke();
            }
        }
    }
}

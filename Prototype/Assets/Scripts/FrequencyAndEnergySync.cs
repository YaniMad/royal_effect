using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FreqEvent : UnityEvent<int> { }

[System.Serializable]
public class EnergyEvent : UnityEvent<float> { }


public class FrequencyAndEnergySync : MonoBehaviour
{
   [SerializeField] private float triggerThreshold = 0.9f;


    [SerializeField] AudioSource referenceAudioSource;
    [SerializeField] private int nbIndex = 512;
    private int sampleRate;
    private float maxFreq;
    private float freqPerIndex;

    [SerializeField] private bool mustComputeReduceSpectrum = false;
    [SerializeField] private bool mustUseBuffer = false;

    [SerializeField] private List<int> freqLimits;

    private float[] reducedSpectrum;
    private float[] reducedSpectrumBuffer;
    private float[] bufferSmooth;

    private float[] spectrum;

    private float[] reducedSpectrumHighest;
    private float[] normalizedReducedSpectrum;
    private float[] normalizedReducedSpectrumBuffer;

    private float[] outputData;
    [SerializeField] private float energy;

    [SerializeField] private FreqEvent freqEventsOn;
    [SerializeField] private FreqEvent freqEventsOff;

    [SerializeField] private EnergyEvent energyEvents;

    /* private int computeEveryNCall = 3;
     private int callCount = 0;*/


    // Start is called before the first frame update
    void Awake()
    {
       // referenceAudioSource = GetComponent<AudioSource>();
        spectrum = new float[nbIndex];

        outputData = new float[nbIndex];

        reducedSpectrum = new float[freqLimits.Count];
        reducedSpectrumBuffer = new float[freqLimits.Count];
        bufferSmooth = new float[freqLimits.Count];

        reducedSpectrumHighest = new float[freqLimits.Count];
        normalizedReducedSpectrum = new float[freqLimits.Count];
        normalizedReducedSpectrumBuffer = new float[freqLimits.Count];

        for (int i = 0; i < freqLimits.Count; ++i)
        {
            reducedSpectrumHighest[i] = 0.00001f;
           // normalizedReducedSpectrumBuffer[i] = 0.00001f;
        }

        Debug.Log(AudioSettings.outputSampleRate);
        Debug.Log(referenceAudioSource.clip.frequency);

        sampleRate = AudioSettings.outputSampleRate;
        maxFreq = sampleRate / 2f;
        freqPerIndex = maxFreq / spectrum.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*callCount++;

        if (callCount%computeEveryNCall != 0)
        {
            return;
        }*/

       

        UpdateSpectrumData();
        UpdateEnergy();

        if (mustComputeReduceSpectrum)
        {
            UpdateReducedSpectrum();
            ComputeBuffer();
            CreateNormalizedSpectrum();      
        }
    }

    void UpdateEnergy()
    {
        referenceAudioSource.GetOutputData(outputData, 0);

        energy = 0;

        for (int i = 0; i < outputData.Length; ++i)
        {
            energy += outputData[i] * outputData[i];
        }

        energy /= nbIndex;
        energy = (float) Math.Sqrt(energy);

        energyEvents.Invoke(energy);

    }

    void CreateNormalizedSpectrum()
    {
        for (int i = 0; i < reducedSpectrum.Length; ++i)
        {
            if (reducedSpectrum[i] > reducedSpectrumHighest[i])
            {
                reducedSpectrumHighest[i] = reducedSpectrum[i];
            }

            normalizedReducedSpectrum[i] = reducedSpectrum[i] / reducedSpectrumHighest[i];
            normalizedReducedSpectrumBuffer[i] = reducedSpectrumBuffer[i] / reducedSpectrumHighest[i];

            if (normalizedReducedSpectrumBuffer[i] < triggerThreshold)
            {
                freqEventsOff.Invoke(i);
            } else
            {
                freqEventsOn.Invoke(i);
            }
        }

    }

    void ComputeBuffer()
    {
        for (int i = 0; i < reducedSpectrum.Length; ++i)
        {
            if (reducedSpectrum[i] > reducedSpectrumBuffer[i])
            {
                reducedSpectrumBuffer[i] = reducedSpectrum[i];
                bufferSmooth[i] = 0.005f;

            } else
            {
               /* reducedFftSamplesBuffer[i] -= reducedFftSamplesBufferDecrease[i];
                reducedFftSamplesBufferDecrease[i] *= 1.2f;*/
                bufferSmooth[i] = (reducedSpectrumBuffer[i] - reducedSpectrum[i]) / 8f;
                reducedSpectrumBuffer[i] -= bufferSmooth[i];

            }
        }
    }

    void UpdateSpectrumData()
    {
        referenceAudioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
    }

    void UpdateReducedSpectrum()
    {
        int j = 0;
        reducedSpectrum[0] = 0f;
        
       for (int i = 0; i < spectrum.Length; i++)
        {
            float currentFreq = (i + 1) * freqPerIndex;

            if (currentFreq > freqLimits[j])
            {
                j++;
                reducedSpectrum[j] = 0f;
            }

            reducedSpectrum[j] += spectrum[i];
        
        }
    }

    public float[] getCurrentFFTSamples()
    {
        return spectrum;
    }

    public float[] getCurrentReducedFFTSamples()
    {
        if (mustUseBuffer)
        {
            return reducedSpectrumBuffer;
        } else
        {
            return reducedSpectrum;
        }
       
    }

    public float[] GetCurrentNormalizedReducedSpectrum()
    {
        if (mustUseBuffer)
        {
            return normalizedReducedSpectrumBuffer;
        }
        else
        {
            return normalizedReducedSpectrum;
        }

    }

    public float GetEnergy()
    {
        return energy;
    }
}

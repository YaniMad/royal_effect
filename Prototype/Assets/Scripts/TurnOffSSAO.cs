using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOffSSAO : MonoBehaviour
{
   public ScriptableRendererFeature ssao;

    public void toggleSSAO(bool _bool)
    {
        ssao.SetActive(_bool);
    }
}


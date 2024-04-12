using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCorrectionFX : PostProcessFX
{
    [Header("FX Settings")]
    [Range(0,5f)]
    public float Exposure = 1;
    [Range(-100f,100f)]
    public float Temperature;
    [Range(-100f,100f)]
    public float Tint;
    [Range(0,10f)]
    public float Contrast = 1;
    [Range(-1,1)]
    public float Brightness = 0;
    public Color ColourFilter;
    [Range(0,5f)]
    public float Saturation = 1;
    [Range(0,5f)]
    public float Gamma = 1;

    public override void ApplyShaderArguments()
    {
        mat.SetFloat("_Exposure", Exposure);
        mat.SetFloat("_Contrast",Contrast);
        mat.SetFloat("_Brightness", Brightness);
        mat.SetFloat("_Saturation", Saturation);
        mat.SetFloat("_Gamma", Gamma);
        mat.SetFloat("_Temperature", Temperature/100f);
        mat.SetFloat("_Tint", Tint/100f);
        mat.SetColor("_ColourFilter", ColourFilter);
    }
}

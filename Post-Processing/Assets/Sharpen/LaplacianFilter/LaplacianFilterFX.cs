using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaplacianFilterFX : PostProcessFX
{
    [Header("FX Settings")]
    public float Sharpness;
    public override void ApplyShaderArguments()
    {
        mat.SetFloat("_Sharpness", Sharpness);
    }
}

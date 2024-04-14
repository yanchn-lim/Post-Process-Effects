using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class GammaCorrectionFX : PostProcessFX
{
    [Range(0,5f)]
    public float Gamma;
    public override void ApplyShaderArguments()
    {
        mat.SetFloat("_Gamma", Gamma);
    }
}

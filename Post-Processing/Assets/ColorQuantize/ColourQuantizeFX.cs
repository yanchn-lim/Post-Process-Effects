using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourQuantizeFX : PostProcessFX
{
    [Range(1,128)]
    public int NumOfColours;

    public override void ApplyShaderArguments()
    {
        mat.SetInt("_numCol", NumOfColours);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShaderFX : PostProcessFX
{
    public float TestArg_1 = 0.5f;
    public override void ApplyShaderArguments()
    {
        mat.SetFloat("_TestArg_1", TestArg_1);
    }
}

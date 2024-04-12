using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsharpMaskFX : PostProcessFX
{
    [Header("FX Settings")]
    public float Radius;
    public float Sharpness;
    RenderTexture originalTexture;
    public override void ApplyShaderArguments()
    {
        mat.SetVector("_MainTex_TexelSize", temp.texelSize);
        mat.SetFloat("_sigma",Radius);
        mat.SetTexture("_originalTexture", originalTexture);
        mat.SetFloat("_w", Sharpness);
    }

    public override void Execute(ref RenderTexture target)
    {
        Initialize(ref target);
        originalTexture = new(target.descriptor);
        Graphics.Blit(target, originalTexture);
        ApplyShaderArguments();
        RunPasses(ref target);
        originalTexture.Release();
    }
}

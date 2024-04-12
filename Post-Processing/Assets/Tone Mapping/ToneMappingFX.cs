using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class ToneMappingFX : PostProcessFX
{
    public ToneMappers ToneMapper;
    public float Cwhite;
    RenderTexture luminanceTexture;

    public override void ApplyShaderArguments()
    {
        Passes[0].PassIndex = (int)ToneMapper;

        mat.SetFloat("_Cwhite", Cwhite);
    }

    public override void Execute(ref RenderTexture target)
    {
        Initialize(ref target);
        luminanceTexture = new(target.descriptor);
        luminanceTexture.name = $"Post-Process Applicator : {Name}_FX_Luminance_RenderTexture";
        ApplyShaderArguments();

        //retrive luminance texture
        Graphics.Blit(target,luminanceTexture,mat,0);
        mat.SetTexture("_LuminanceTex", luminanceTexture);
        RunPasses(ref target);
        luminanceTexture.Release();
    }

}
public enum ToneMappers
{
    RGBCLAMP = 1,
    REINHARD,
    REINHARD_EXTENDED,
    HABLE,
}

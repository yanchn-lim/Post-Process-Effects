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

    public float _A = 0.15f;
    public float _B = 0.5f;
    public float _C = 0.1f;
    public float _D = 0.2f;
    public float _E = 0.02f;
    public float _F = 0.3f;
    public float _W = 11.2f;

    public float Nark_A = 2.51f;
    public float Nark_B = 0.03f;
    public float Nark_C = 2.43f;
    public float Nark_D = 0.59f;
    public float Nark_E = 0.14f;

    public float Tumblin_Ldmax;
    public float Tumblin_Cmax;

    public override void ApplyShaderArguments()
    {
        Passes[0].PassIndex = (int)ToneMapper;

        mat.SetFloat("_Cwhite", Cwhite);
        mat.SetFloat("_A", _A);
        mat.SetFloat("_B", _B);
        mat.SetFloat("_C", _C);
        mat.SetFloat("_D", _D);
        mat.SetFloat("_E", _E);
        mat.SetFloat("_F", _F);
        mat.SetFloat("_W", _W);

        mat.SetFloat("KACES_A", Nark_A);
        mat.SetFloat("KACES_B", Nark_B);
        mat.SetFloat("KACES_C", Nark_C);
        mat.SetFloat("KACES_D", Nark_D);
        mat.SetFloat("KACES_E", Nark_E);

        mat.SetFloat("_Ldmax", Tumblin_Ldmax);
        mat.SetFloat("_Cmax", Tumblin_Cmax);

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
    NARKOWICZ_ACES,
    TUMBLIN_RUSHMEIER
}

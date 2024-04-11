using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessApplicator : MonoBehaviour
{
    [Header("FX SETTINGS")]
    public PostProcessFX[] Effects;
    public bool ApplyFx;
    public Texture test;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!ApplyFx)
        {
            Graphics.Blit(source, destination);
            return;
        }

        Graphics.Blit(ApplyPostProcessing(source), destination);
    }

    public RenderTexture ApplyPostProcessing(RenderTexture source)
    {
        RenderTexture target = new(source.descriptor);
        Graphics.Blit(source, target);
        foreach (var fx in Effects)
        {
            if (!fx.Active)
                continue;
            fx.Execute(target, ref target);
        }

        return target;
    }
}

[System.Serializable]
public class PostProcessFX
{
    public string Name;
    public Shader shader;
    public bool Active;
    public Pass[] Passes;
    Material mat;
    public void Execute(RenderTexture source, ref RenderTexture target)
    {
        mat = new(shader);
        RenderTexture temp = new(source.descriptor);
        Graphics.Blit(source, temp);

        foreach (var pass in Passes)
        {
            if (!pass.Active)
                continue;

            Graphics.Blit(temp, target, mat, pass.PassIndex);
            Graphics.Blit(target, temp);
        }
    }
}

[System.Serializable]
public struct Pass
{
    public string Name;
    public int PassIndex;
    public bool Active;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessApplicator : MonoBehaviour
{
    [Header("FX SETTINGS")]
    public bool ApplyFx;
    public PostProcessFX[] Effects;
    public Texture test;
    RenderTexture target;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        target = new(source.descriptor);
        target.name = "Post-Process Applicator : Temp_Target_RenderTexture";
        if (!ApplyFx)
        {
            Graphics.Blit(source, destination);
            return;
        }

        Graphics.Blit(ApplyPostProcessing(source), destination);

        target.Release();
    }
    
    public RenderTexture ApplyPostProcessing(RenderTexture source)
    {
        Graphics.Blit(source, target);
        foreach (var fx in Effects)
        {
            if (!fx.Active)
                continue;
            fx.Execute(ref target);
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
    RenderTexture temp;

    public void Execute(ref RenderTexture target)
    {
        mat = new(shader);
        temp = new(target.descriptor);
        temp.name = $"Post-Process Applicator : {Name}_FX_Temp_RenderTexture";
        foreach (var pass in Passes)
        {
            if (!pass.Active)
                continue;

            Graphics.Blit(target, temp, mat, pass.PassIndex);
            Graphics.Blit(temp, target);
        }
        temp.Release();
        //UnityEngine.MonoBehaviour.DestroyImmediate(mat);
    }
}

[System.Serializable]
public struct Pass
{
    public string Name;
    public int PassIndex;
    public bool Active;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, ExecuteInEditMode]
public class PostProcessFX : MonoBehaviour
{
    public string Name;
    public Shader shader;
    public bool Active;
    public Pass[] Passes;

    protected Material mat;
    protected RenderTexture temp;

    protected void RunPasses(ref RenderTexture target)
    {
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

    protected void Initialize(ref RenderTexture target)
    {
        mat = new(shader);
        temp = new(target.descriptor);
        temp.name = $"Post-Process Applicator : {Name}_FX_Temp_RenderTexture";
        Name = shader.name.Remove(0,7);
    }

    public virtual void ApplyShaderArguments()
    {

    }

    public virtual void Execute(ref RenderTexture target)
    {
        Initialize(ref target);
        ApplyShaderArguments();
        RunPasses(ref target);
    }
}

[System.Serializable]
public struct Pass
{
    public string Name;
    public int PassIndex;
    public bool Active;
}

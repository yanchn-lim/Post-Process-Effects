using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class PostProcessApplicator : MonoBehaviour
{
    [Header("FX SETTINGS")]
    public bool ApplyFx;
    PostProcessFX[] effects;
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
        effects = GameObject.Find("PostProcessFX").GetComponentsInChildren<PostProcessFX>();
        Graphics.Blit(source, target);
        foreach (var fx in effects)
        {
            if (!fx.Active)
                continue;
            fx.Execute(ref target);
        }

        return target;
    }
}

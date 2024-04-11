using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEditor;

public class ImageBlitter : MonoBehaviour
{
    public Texture[] Images;
    int currentIndex = 0;
    public bool ShowImage;
    public PostProcessFX[] Effects;
    

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(ShowImage)
            Graphics.Blit(Images[currentIndex],destination);
    }

    public void GoNextImage()
    {
        currentIndex++;
        if (currentIndex == Images.Length)
        {
            currentIndex = 0;
        }
    }

    public void ApplyPostProcessing(RenderTexture source)
    {
        RenderTexture render = new(source.descriptor);
    }
}

[System.Serializable]
public class PostProcessFX
{
    public Shader shader;
    public Pass[] Passes;
    Material mat;
    public void Execute(RenderTexture source)
    {
        mat = new(shader);
        RenderTexture temp = new(source.descriptor);
        Graphics.Blit(source, temp);


        foreach (var pass in Passes)
        {
            if (!pass.Active)
                continue;

            Graphics.Blit(temp, temp, mat,pass.PassIndex);
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


[CustomEditor(typeof(ImageBlitter))]
class ImageBlitterUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ImageBlitter ib = (ImageBlitter)target;
        if (GUILayout.Button("SwitchImage"))
        {
            ib.GoNextImage();
        }
    }
}
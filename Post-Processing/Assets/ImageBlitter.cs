using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ImageBlitter : MonoBehaviour
{
    [Header("IMAGE SETTINGS")]
    public Texture[] Images;
    int currentIndex = 0;
    public bool ShowImage;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (ShowImage)
        {
            Graphics.Blit(Images[currentIndex], destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void GoNextImage()
    {
        currentIndex++;
        if (currentIndex == Images.Length)
        {
            currentIndex = 0;
        }
    }
}

[CustomEditor(typeof(ImageBlitter))]
class ImageBlitterUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ImageBlitter ib = (ImageBlitter)target;
        GUILayout.Space(10);
        if (GUILayout.Button("SwitchImage"))
        {
            ib.GoNextImage();
        }
    }
}
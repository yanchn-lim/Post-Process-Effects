using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SwitchImage : MonoBehaviour
{
    [SerializeField]
    Material mat;
    [SerializeField]
    Texture[] Images;
    int currentIndex = 0;

    public void GoNextImage()
    {
        currentIndex++;
        if (currentIndex == Images.Length)
        {
            currentIndex = 0;
        }
        mat.mainTexture = Images[currentIndex];
    }
}

[CustomEditor(typeof(SwitchImage))]
class SwitchImageEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SwitchImage si = (SwitchImage)target;
        if (GUILayout.Button("SwitchImage"))
        {
            si.GoNextImage();
        }
    }
}

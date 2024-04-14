using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PixelizeFX : PostProcessFX
{
    public int Power;
    public Vector2Int AspectRatio;
    protected override void Initialize(ref RenderTexture target)
    {
        Name = "Pixelize";

        Vector2Int res = new(Power * AspectRatio.x, Power * AspectRatio.y);
        //res /= Downsample;
        
        temp = new(res.x,res.y,0);
        temp.name = $"Post-Process Applicator : {Name}_FX_Temp_RenderTexture";
        temp.format = target.format;
        temp.antiAliasing = 1;
        temp.anisoLevel = 1;
        
    }

    public override void Execute(ref RenderTexture target)
    {
        Initialize(ref target);
        Graphics.Blit(target, temp);
        Graphics.Blit(temp, target);
        temp.Release();
    }
}

[CustomEditor(typeof(PixelizeFX))]
public class PixelizeFXUI : Editor
{
    public override void OnInspectorGUI()
    {
        PixelizeFX tar = (PixelizeFX)target;
        EditorGUILayout.LabelField($"{tar.Name}",EditorStyles.boldLabel); 
        GUILayout.Space(5);
        tar.Active = EditorGUILayout.Toggle("Active",tar.Active);

        GUILayout.Space(10);
        tar.Power = EditorGUILayout.IntSlider("Power",tar.Power,1,128);
        tar.AspectRatio = EditorGUILayout.Vector2IntField("Aspect Ratio",tar.AspectRatio);
        GUILayout.Space(10);

        EditorGUILayout.LabelField("Resolution", $"{tar.Power * tar.AspectRatio.x} x {tar.Power * tar.AspectRatio.y}");
    }
}
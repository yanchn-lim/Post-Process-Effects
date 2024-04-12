using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ToneMappingFX))]
public class ToneMapFXUI : Editor
{
    public VisualTreeAsset inspectorXML;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new();

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Tone Mapping/ToneMapFXEditor.uxml");
        visualTree.CloneTree(inspector);

        return inspector;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ToneMappingFX))]
public class ToneMapFXUI : Editor
{
    bool check = false;

    public override void OnInspectorGUI()
    {
        if (!check)
            return;

        ToneMappingFX tar = (ToneMappingFX)target;
#pragma warning disable CS0618 // Type or member is obsolete
        tar.shader = (Shader)EditorGUILayout.ObjectField("Shader",tar.shader,typeof(Shader));
#pragma warning restore CS0618 // Type or member is obsolete
        tar.Active = EditorGUILayout.Toggle("Active",tar.Active);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("FX Settings", EditorStyles.boldLabel);
        tar.ToneMapper = (ToneMappers)EditorGUILayout.EnumPopup("Tone Mapper", tar.ToneMapper);
        EditorGUILayout.LabelField("Current Pass Index: ", $"{(int)tar.ToneMapper}");


        EditorGUILayout.Space(10);
        switch (tar.ToneMapper)
        {
            case ToneMappers.RGBCLAMP:
                EditorGUILayout.LabelField("RGBCLAMP", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("No parameters...");

                break;

            case ToneMappers.REINHARD:
                EditorGUILayout.LabelField("REINHARD", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("No parameters...");

                break;

            case ToneMappers.REINHARD_EXTENDED:
                EditorGUILayout.LabelField("REINHARD EXTENDED", EditorStyles.boldLabel);
                tar.Cwhite = EditorGUILayout.FloatField("Cwhite",tar.Cwhite);
                break;

            case ToneMappers.HABLE:
                EditorGUILayout.LabelField("HABLE", EditorStyles.boldLabel);
                tar._A = EditorGUILayout.FloatField("A", tar._A);
                tar._B = EditorGUILayout.FloatField("B", tar._B);
                tar._C = EditorGUILayout.FloatField("C", tar._C);
                tar._D = EditorGUILayout.FloatField("D", tar._D);
                tar._E = EditorGUILayout.FloatField("E", tar._E);
                tar._F = EditorGUILayout.FloatField("F", tar._F);
                tar._W = EditorGUILayout.FloatField("W", tar._W);

                if (GUILayout.Button("Reset Value"))
                {
                    tar._A = 0.15f;
                    tar._B = 0.5f;
                    tar._C = 0.1f;
                    tar._D = 0.2f;
                    tar._E = 0.02f;
                    tar._F = 0.3f;
                    tar._W = 11.2f;
                }
                break;

            case ToneMappers.NARKOWICZ_ACES:
                EditorGUILayout.LabelField("NARKOWICZ ACES", EditorStyles.boldLabel);

                tar.Nark_A = EditorGUILayout.FloatField("A", tar.Nark_A);
                tar.Nark_B = EditorGUILayout.FloatField("B", tar.Nark_B);
                tar.Nark_C = EditorGUILayout.FloatField("C", tar.Nark_C);
                tar.Nark_D = EditorGUILayout.FloatField("D", tar.Nark_D);
                tar.Nark_E = EditorGUILayout.FloatField("E", tar.Nark_E);
                if (GUILayout.Button("Reset Value"))
                {
                    tar.Nark_A = 2.51f;
                    tar.Nark_B = 0.03f;
                    tar.Nark_C = 2.43f;
                    tar.Nark_D = 0.59f;
                    tar.Nark_E = 0.14f;
                }
                break;
            case ToneMappers.TUMBLIN_RUSHMEIER:
                EditorGUILayout.LabelField("TUMBLIN RUSHMEIER", EditorStyles.boldLabel);
                tar.Tumblin_Ldmax = EditorGUILayout.FloatField("Ldmax", tar.Tumblin_Ldmax);
                tar.Tumblin_Cmax = EditorGUILayout.FloatField("Cmax", tar.Tumblin_Cmax);

                break;
            default:
                break;
        }

        //DrawDefaultInspector();
    }
}

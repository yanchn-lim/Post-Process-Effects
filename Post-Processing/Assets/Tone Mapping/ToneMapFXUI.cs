using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ToneMappingFX))]
public class ToneMapFXUI : Editor
{

    public override void OnInspectorGUI()
    {
        ToneMappingFX tar = (ToneMappingFX)target;
#pragma warning disable CS0618 // Type or member is obsolete
        tar.shader = (Shader)EditorGUILayout.ObjectField("Shader",tar.shader,typeof(Shader));
#pragma warning restore CS0618 // Type or member is obsolete
        tar.Active = EditorGUILayout.Toggle("Active",tar.Active);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("FX Settings", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Current Pass Index: ", $"{(int)tar.ToneMapper}");
        tar.ToneMapper = (ToneMappers)EditorGUILayout.EnumPopup("Tone Mapper", tar.ToneMapper);
        tar.ViewLuminance = EditorGUILayout.Toggle("View Luminance", tar.ViewLuminance);

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
                EditorGUILayout.LabelField("A", $"{tar._A}");
                EditorGUILayout.LabelField("B", $"{tar._B}");
                EditorGUILayout.LabelField("C", $"{tar._C}");
                EditorGUILayout.LabelField("D", $"{tar._D}");
                EditorGUILayout.LabelField("E", $"{tar._E}");
                EditorGUILayout.LabelField("F", $"{tar._F}");
                EditorGUILayout.LabelField("W", $"{tar._W}");

                //tar._A = EditorGUILayout.FloatField("A", tar._A);
                //tar._B = EditorGUILayout.FloatField("B", tar._B);
                //tar._C = EditorGUILayout.FloatField("C", tar._C);
                //tar._D = EditorGUILayout.FloatField("D", tar._D);
                //tar._E = EditorGUILayout.FloatField("E", tar._E);
                //tar._F = EditorGUILayout.FloatField("F", tar._F);
                //tar._W = EditorGUILayout.FloatField("W", tar._W);

                //if (GUILayout.Button("Reset Value"))
                //{
                //    tar._A = 0.15f;
                //    tar._B = 0.5f;
                //    tar._C = 0.1f;
                //    tar._D = 0.2f;
                //    tar._E = 0.02f;
                //    tar._F = 0.3f;
                //    tar._W = 11.2f;
                //}
                break;

            case ToneMappers.NARKOWICZ_ACES:
                EditorGUILayout.LabelField("NARKOWICZ ACES", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("A", $"{tar.Nark_A}");
                EditorGUILayout.LabelField("B", $"{tar.Nark_B}");
                EditorGUILayout.LabelField("C", $"{tar.Nark_C}");
                EditorGUILayout.LabelField("D", $"{tar.Nark_D}");
                EditorGUILayout.LabelField("E", $"{tar.Nark_E}");

                //tar.Nark_A = EditorGUILayout.FloatField("A", tar.Nark_A);
                //tar.Nark_B = EditorGUILayout.FloatField("B", tar.Nark_B);
                //tar.Nark_C = EditorGUILayout.FloatField("C", tar.Nark_C);
                //tar.Nark_D = EditorGUILayout.FloatField("D", tar.Nark_D);
                //tar.Nark_E = EditorGUILayout.FloatField("E", tar.Nark_E);
                //if (GUILayout.Button("Reset Value"))
                //{
                //    tar.Nark_A = 2.51f;
                //    tar.Nark_B = 0.03f;
                //    tar.Nark_C = 2.43f;
                //    tar.Nark_D = 0.59f;
                //    tar.Nark_E = 0.14f;
                //}
                break;
            case ToneMappers.HILL_ACES:
                EditorGUILayout.LabelField("HILL ACES", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("No parameters...");

                break;
            case ToneMappers.TUMBLIN_RUSHMEIER:
                EditorGUILayout.LabelField("TUMBLIN RUSHMEIER", EditorStyles.boldLabel);
                tar.Tumblin_Ldmax = EditorGUILayout.Slider("Ldmax",tar.Tumblin_Ldmax, 0, 10);
                tar.Tumblin_Cmax = EditorGUILayout.Slider("Cmax",tar.Tumblin_Cmax, 0, 100);

                break;

            case ToneMappers.SCHLICK:
                EditorGUILayout.LabelField("SCHLICK", EditorStyles.boldLabel);
                tar.Schlick_P = EditorGUILayout.Slider("P",tar.Schlick_P, 0, 100);
                tar.Schlick_HiVal = EditorGUILayout.Slider("HiVal",tar.Schlick_HiVal, 0, 100);
                break;

            case ToneMappers.USHIMURA:
                EditorGUILayout.LabelField("SCHLICK", EditorStyles.boldLabel);
                tar.Uchimura_M = EditorGUILayout.Slider("M",tar.Uchimura_M, 0, 3);
                tar.Uchimura_a = EditorGUILayout.Slider("a",tar.Uchimura_a, 0, 3);
                tar.Uchimura_m = EditorGUILayout.Slider("m",tar.Uchimura_m, 0, 3);
                tar.Uchimura_l = EditorGUILayout.Slider("l",tar.Uchimura_l, 0, 3);
                tar.Uchimura_c = EditorGUILayout.Slider("c",tar.Uchimura_c, 0, 3);
                tar.Uchimura_b = EditorGUILayout.Slider("b",tar.Uchimura_b, -3, 0);

                break;

            case ToneMappers.WARD:
                EditorGUILayout.LabelField("WARD", EditorStyles.boldLabel);
                tar.Ward_Ldmax = EditorGUILayout.Slider("Ldmax", tar.Ward_Ldmax, 0, 5);

                break;
            default:
                break;
        }

        //DrawDefaultInspector();
    }
}

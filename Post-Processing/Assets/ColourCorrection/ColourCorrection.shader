Shader "Hidden/ColourCorrection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Contrast;
            float _Brightness;
            float _Saturation;
            float _Exposure;
            float _Temperature;
            float _Tint;
            float4 _ColourFilter;

            //from unity's white balance node in shadergraph
            //look to implement my own
            float3 WhiteBalance(float3 col,float temp,float tint){
                float t1 = temp * 10.0f / 6.0f;
                float t2 = tint * 10.0f / 6.0f;

                float x = 0.31271 - t1 * (t1 < 0 ? 0.1 : 0.05);
                float standardIlluminantY = 2.87 * x - 3 * x * x - 0.27509507;
                float y = standardIlluminantY + t2 * 0.05;

                float3 w1 = float3(0.949237, 1.03542, 1.08728);


                float Y = 1;
                float X = Y * x / y;
                float Z = Y * (1 - x - y) / y;
                float L = 0.7328 * X + 0.4296 * Y - 0.1624 * Z;
                float M = -0.7036 * X + 1.6975 * Y + 0.0061 * Z;
                float S = 0.0030 * X + 0.0136 * Y + 0.9834 * Z;
                float3 w2 = float3(L, M, S);

                float3 balance = float3(w1.x / w2.x, w1.y / w2.y, w1.z / w2.z);

                float3x3 LIN_2_LMS_MAT = {
                    3.90405e-1, 5.49941e-1, 8.92632e-3,
                    7.08416e-2, 9.63172e-1, 1.35775e-3,
                    2.31082e-2, 1.28021e-1, 9.36245e-1
                };

                float3x3 LMS_2_LIN_MAT = {
                    2.85847e+0, -1.62879e+0, -2.48910e-2,
                    -2.10182e-1,  1.15820e+0,  3.24281e-4,
                    -4.18120e-2, -1.18169e-1,  1.06867e+0
                };

                //convert linear rgb to LMS color space
                float3 lms = mul(LIN_2_LMS_MAT, col);
                lms *= balance;
                //convert LMS to rgb color space
                return mul(LMS_2_LIN_MAT, lms);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                float3 col = c.rgb;

                //exposure
                col *= _Exposure;
                col = max(0.0f,col);

                //temperature
                col = WhiteBalance(col,_Temperature,_Tint);
                col = max(0.0f,col);

                //contrast + brightness
                col = _Contrast * (col-0.5) + 0.5 + _Brightness;
                col = max(0.0f,col);

                //colour filtering
                col *= _ColourFilter;
                col = max(0.0f,col);

                //saturation
                float gs = dot(col,float4(0.299,0.587,0.114,0));
                col = lerp(gs,col,_Saturation);
                col = max(0.0f,col);

                // //gamma
                // col = pow(col,_Gamma);
                // col = max(0.0f,col);

                return fixed4(col,c.a);
            }
            ENDCG
        }
    }
}

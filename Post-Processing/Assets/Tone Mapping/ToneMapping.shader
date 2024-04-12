Shader "Hidden/ToneMapping"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        CGINCLUDE
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

        float luminance(float3 col){
            return dot(col.rgb,float3(0.299f,0.587f,0.114f));
        }

        sampler2D _MainTex,_LuminanceTex;
        float _b;
        



        ENDCG

        Pass
        {
            Name "Luminance Retrieval Pass"
            CGPROGRAM

            float frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                return luminance(col);
            }
            ENDCG
        }

        Pass
        {
            Name "RGB Clamp Pass"
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
    
                return saturate(col);
            }
               
            ENDCG
        }

        Pass{
            Name "Reinhard Pass"

            CGPROGRAM

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float L_in = luminance(col);
                float L_out = L_in / (1.0f + L_in);
                float3 Col_out = col / L_in * L_out;

                return float4(saturate(Col_out),1.0f);
            }

            ENDCG
        }

        Pass
        {
            Name "Reinhard Extended Pass"
            CGPROGRAM

            float _Cwhite;
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float L_in = luminance(col);
                float L_out = (L_in * (1.0f + L_in/(_Cwhite*_Cwhite))) / (1.0+L_in);
                float3 Col_out = col / L_in * L_out;

                return float4(saturate(Col_out),1.0f);
            }
               
            ENDCG
        }

        Pass
        {
            Name "Hable Pass"
            CGPROGRAM

            float _A,_B,_C,_D,_E,_F,_W;

            float3 uncharted2Tonemap(float3 x){
                return ((x*(_A * x + _C * _B) + _D * _E) / (x * (_A * x + _B) + _D * _E)) - _E / _F;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float ExposureBias = 2.0f;
                float3 curr = ExposureBias * uncharted2Tonemap(col);
                float3 whiteScale = 1.0f / uncharted2Tonemap(float3(_W,_W,_W));

                float3 Cout = curr * whiteScale;

                return float4(saturate(Cout),1.0f);
            }
               
            ENDCG
        }

    }
}

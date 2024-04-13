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
                return ((x*(_A * x + _C * _B) + _D * _E) / (x * (_A * x + _B) + _D * _F)) - _E / _F;
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

        Pass
        {
            Name "Narkowicz ACES Pass"
            CGPROGRAM

            float KACES_A,KACES_B,KACES_C,KACES_D,KACES_E;

            float3 NarkowiczACES(float3 x){
                return saturate((x * (KACES_A * x + KACES_B)) / (x * (KACES_C * x + KACES_D) + KACES_E));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                return float4(NarkowiczACES(col),1.0f);
            }
               
            ENDCG
        }

        Pass
        {
            Name "Tumblin Rushmeier Pass"
            CGPROGRAM

            float _Ldmax,_Cmax;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float Lin = luminance(col);

                float Lavg = tex2Dlod(_LuminanceTex, float4(i.uv.x, i.uv.y, 0, 10.0f)).r;

                float logLrw = log10(Lavg) + 0.84;
                float alphaRw = 0.4 * logLrw + 2.92;
                float betaRw = -0.4 * logLrw * logLrw - 2.584 * logLrw + 2.0208;
                float Lwd = _Ldmax / sqrt(_Cmax);
                float logLd = log10(Lwd) + 0.84;
                float alphaD = 0.4 * logLd + 2.92;
                float betaD = -0.4 * logLd * logLd - 2.584 * logLd + 2.0208;
                float Lout = pow(Lin, alphaRw / alphaD) / _Ldmax * pow(10.0, (betaRw - betaD) / alphaD) - (1.0 / _Cmax);

                float3 Cout = col / Lin * Lout;

                return fixed4(saturate(Cout), 1.0f);
            }
               
            ENDCG
        }

    }
}

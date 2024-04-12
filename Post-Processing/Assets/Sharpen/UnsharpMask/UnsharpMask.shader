Shader "Hidden/UnsharpMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _originalTexture("OGTexture",2D) = "white"{}
        _MainTex_TexelSize ("Texel Size",Vector) = (0,0,0,0)
        _sigma ("Sigma",Float) = 0
        _w ("W", Float) = 0
    }
    SubShader
    {
        CGINCLUDE
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #define PI 3.1415926535

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

        sampler2D _MainTex;
        sampler2D _originalTexture;
        float4 _MainTex_TexelSize;
        float _sigma;
        float _w;
        
        float gaussian(int x)
        {
            float sigSq = _sigma * _sigma;
            return (1.0f/sqrt(PI * 2 * sigSq)) * exp(-(x*x)/(2.0f*sigSq));
        }

        ENDCG

        Pass
        {
            Name "Blur Pass Horizontal"
            CGPROGRAM
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int kernelRadius = max(1.0f,floor(_sigma * 2.45f));
                float4 c = 0;
                float kernelSum = 0;
                for(int x = -kernelRadius; x <= kernelRadius; x++){
                    fixed4 col = tex2D(_MainTex, i.uv + float2(x,0) * _MainTex_TexelSize.xy);
                    float gauss = gaussian(x);
                    c += col * gauss;
                    kernelSum += gauss;
                }

                return c / kernelSum;
            }
            ENDCG
        }
        Pass
        {
            Name "Blur Pass Vertical"
            CGPROGRAM
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                int kernelRadius = max(1.0f,floor(_sigma * 2.45f));
                float4 c = 0;
                float kernelSum = 0;
                for(int y = -kernelRadius; y <= kernelRadius; y++){
                    fixed4 col = tex2D(_MainTex, i.uv + float2(0,y) * _MainTex_TexelSize.xy);
                    float gauss = gaussian(y);
                    c += col * gauss;
                    kernelSum += gauss;
                }

                return c / kernelSum;
            }
            ENDCG
        }
        Pass
        {
            Name "Unsharp Mask Pass"
            CGPROGRAM
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 blur = tex2D(_MainTex,i.uv);
                float4 col = tex2D(_originalTexture,i.uv);

                return saturate(((1 + _w) * col) - (_w * blur));
            }
            ENDCG
        }
    }
}

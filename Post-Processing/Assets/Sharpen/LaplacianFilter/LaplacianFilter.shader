Shader "Hidden/LaplacianFilter"
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
            float _Sharpness;
            float4 _MainTex_TexelSize;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 x = float2(_MainTex_TexelSize.x,0);
                float2 y = float2(0,_MainTex_TexelSize.y);

                fixed4 t = tex2D(_MainTex, i.uv + y) * _Sharpness * -1;
                fixed4 b = tex2D(_MainTex, i.uv - y) * _Sharpness * -1;
                fixed4 l = tex2D(_MainTex, i.uv - x) * _Sharpness * -1;
                fixed4 r = tex2D(_MainTex, i.uv + x) * _Sharpness * -1;

                fixed4 neighbourPixel = t + b + l + r;
                fixed4 c = col * 4 * _Sharpness + 1 + neighbourPixel;

                return saturate(col * c);
            }
            ENDCG
        }
    }
}

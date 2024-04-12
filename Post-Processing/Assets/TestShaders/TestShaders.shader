Shader "Hidden/TestShaders"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TestArg_1("Test Arg",Float) = 1
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

        float _TestArg_1;
        ENDCG

        Pass
        {
            CGPROGRAM
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = 1 - col;
                col *= _TestArg_1;
                return col;
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = 1 - col;
                return col;
            }
            ENDCG
        }
    }
}

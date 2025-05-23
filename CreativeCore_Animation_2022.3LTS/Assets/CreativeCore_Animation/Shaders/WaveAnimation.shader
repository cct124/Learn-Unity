Shader "Custom/WaveAnimation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _WaveSpeed ("Wave Speed", Range(0.1, 10)) = 1
        _WaveHeight ("Wave Height", Range(0, 1)) = 0.1
        _WaveFrequency ("Wave Frequency", Range(0.1, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _WaveSpeed;
            float _WaveHeight;
            float _WaveFrequency;

            v2f vert (appdata v)
            {
                v2f o;
                
                // 创建顶点动画 - 波浪效果
                float3 modifiedVertex = v.vertex;
                modifiedVertex.y += sin((_Time.y * _WaveSpeed) + (v.vertex.x * _WaveFrequency)) * _WaveHeight;
                
                o.vertex = UnityObjectToClipPos(modifiedVertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}

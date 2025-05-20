Shader "Custom/DissolveEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _EdgeColor1 ("Edge Color 1", Color) = (1,0,0,1)
        _EdgeColor2 ("Edge Color 2", Color) = (1,1,0,1)
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.5
        _EdgeWidth ("Edge Width", Range(0, 0.2)) = 0.05
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
                float2 noiseUV : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float4 _EdgeColor1;
            float4 _EdgeColor2;
            float _DissolveAmount;
            float _EdgeWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 采样主纹理和噪声纹理
                fixed4 mainTex = tex2D(_MainTex, i.uv);
                float noise = tex2D(_NoiseTex, i.noiseUV).r;
                
                // 计算溶解边缘
                float dissolveEdge = noise - _DissolveAmount;
                
                // 裁剪低于阈值的像素（实现溶解效果）
                clip(dissolveEdge);
                
                // 边缘效果
                if (dissolveEdge < _EdgeWidth)
                {
                    float edgeLerp = dissolveEdge / _EdgeWidth;
                    return lerp(_EdgeColor1, _EdgeColor2, edgeLerp);
                }
                
                return mainTex;
            }
            ENDCG
        }
    }
}

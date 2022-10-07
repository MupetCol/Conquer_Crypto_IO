Shader "URP_Coded/USB_Function_Clamp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Xvalue ("X", Range(0,1)) = 0
        _Avalue("A", Range(0,1)) = 0
        _Bvalue("B", Range(0,1)) = 0
        
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "RenderPipeline"="UniversalRenderPipeline"
        }
        
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "HLSLSupport.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" 

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
            float _Xvalue;
            float _Avalue;
            float _Bvalue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float ourClamp(float a, float x, float b)
            {
                return max(a, min(x, b));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float darkness = ourClamp(_Avalue, _Xvalue, _Bvalue);
                fixed4 col = tex2D(_MainTex, i.uv) * darkness;
                return col;
            }
            ENDHLSL
        }
    }
}

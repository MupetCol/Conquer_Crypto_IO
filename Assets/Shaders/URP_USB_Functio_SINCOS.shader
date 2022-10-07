Shader "URP_Coded/USB_Function_SINCOS"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Rotation Speed", Range(0,3)) = 1
        
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
            float _Speed;

            // let's add our rotation function
            float3 rotation(float3 vertex)
            {
                // let’s add the rotation variables
                float c = cos(_Time.y * _Speed);
                float s = sin(_Time.y * _Speed);

                // create a three-dimensional matrix
                float3x3 m = float3x3
                (
                    // Depending on where we use sin and cos it would cause different rotations
                    c, 0, s,
                    0, 1, 0,
                    -s, 0, c
                );
                // let’s multiply the matrix times the vertex input
                 return mul(m, vertex);
            }
            v2f vert (appdata v)
            {
                v2f o;
                float3 rotVertex = rotation(v.vertex);
                o.vertex = TransformObjectToHClip(rotVertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDHLSL
        }
    }
}

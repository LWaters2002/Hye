Shader "Unlit/TestShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BaseColor ("Color", Color) = (1,1,1,1)
        _ObjectPosition ("Object Position", Vector) = (0,0,0,0)
    }

    SubShader
    {
        Cull off
        Tags { "RenderType"="Opaque"}
        LOD 200

        HLSLINCLUDE 
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _BaseColor;
            float4 _ObjectPosition;
        CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        struct VertexInput
        {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VertexOutput
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        ENDHLSL
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            VertexOutput vert(VertexInput i)
            {
                VertexOutput o;
                o.position = TransformObjectToHClip(i.position.xyz);
                o.uv = i.uv;

                float2 targetFloat = float2(_ObjectPosition.x,_ObjectPosition.z);
                float2 posFloat = float2(i.position.x, i.position.z);
                float lengthToTarget = distance(posFloat,targetFloat);
                float steppedL = smoothstep(1.5,0.8,lengthToTarget);

                o.position.y -= steppedL*2.0;

                return o;
            }

            float4 frag(VertexOutput i) : SV_TARGET
            {
                float4 baseTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                return baseTex * _BaseColor;
            }

            ENDHLSL
        }
    }
}

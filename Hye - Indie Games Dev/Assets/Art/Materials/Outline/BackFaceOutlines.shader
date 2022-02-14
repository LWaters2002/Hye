Shader "Outlines/BackFaceOutlines"
{
    Properties
    {
        _Thickness("Thickness", float) = 1 //Mesh extrusion amount
        _Color("Color", Color) = (1,1,1,1) // Colour of outline
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100

        Pass
        {
            Name "Outlines"

            Cull front

            HLSLPROGRAM
            //URP requirements
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            //Register functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            //Logic file
            #include "BackFaceOutlines.hlsl"

            ENDHLSL
        }
    }
}

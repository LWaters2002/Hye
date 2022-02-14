#ifndef BACKFACEOUTLINES_INCLUDED
#define BACKFACEOUTLINES_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

struct Attributes 
{
    float4 positionOS       : POSITION; //Object space position
    float3 normalOS         : NORMAL;   // Normal Vector in object space
};

struct VertexOutput
{
    float4 positionCS   : SV_POSITION; //position in clip-space
};

float _Thickness;
float4 _Color;

VertexOutput Vertex(Attributes input)
{
    VertexOutput output = (VertexOutput)0;

    float3 normalOS = input.normalOS;

    //Extrude object space along normal
    float3 posOS = input.positionOS.xyz + normalOS * _Thickness;
    // Convert calculated position to clip sapce
    output.positionCS = GetVertexPositionInputs(posOS).positionCS;

    return output;
} 

float4 Fragment(VertexOutput input) : SV_Target 
{
    return _Color;
}
#endif
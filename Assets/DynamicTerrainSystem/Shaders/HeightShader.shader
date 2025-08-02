Shader "Custom/HeightShader"
{
    Properties
    {
        _MinHeight ("Min Height", Float) = 0
        _MaxHeight ("Max Height", Float) = 100
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "RenderPipeline"="UniversalPipeline"
        }
        
        Pass
        {
            Name "HeightmapPass"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };
            
            CBUFFER_START(UnityPerMaterial)
                float _MinHeight;
                float _MaxHeight;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionHCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                
                return output;
            }
            
            float4 frag(Varyings input) : SV_Target
            {
                float worldY = input.positionWS.y;
                float normalizedHeight = saturate((worldY - _MinHeight) / (_MaxHeight - _MinHeight)) * 0.5;
                
                return float4(normalizedHeight, normalizedHeight, normalizedHeight, 1.0);
            }
            ENDHLSL
        }
    }
}
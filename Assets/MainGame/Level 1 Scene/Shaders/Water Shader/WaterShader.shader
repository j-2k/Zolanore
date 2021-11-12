Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
        _SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
    _FoamDistance("Foam Distance", Float) = 0.4
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 100
Blend SrcAlpha OneMinusSrcAlpha
ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 noiseUV : TEXCOORD0;
                float4 screenPosition : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;
            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;
sampler2D _SurfaceNoise;
float4 _SurfaceNoise_ST;
float _SurfaceNoiseCutoff;
float _FoamDistance;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.noiseUV = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 sampleTex = tex2D(_MainTex, i.uv);
                float linearDepth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r);

                float depthDifference = linearDepth - i.screenPosition.w;

                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);

                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);
                float surfaceNoiseSample = tex2D(_SurfaceNoise, i.noiseUV).r;
                
                float foamDepthDifference01 = saturate(depthDifference / _FoamDistance);
float surfaceNoiseCutoff = foamDepthDifference01 * _SurfaceNoiseCutoff;

float surfaceNoise = surfaceNoiseSample > surfaceNoiseCutoff ? 1 : 0;

return waterColor + surfaceNoise;



                //return sampleTex;
            }
            ENDCG
        }
    }
}

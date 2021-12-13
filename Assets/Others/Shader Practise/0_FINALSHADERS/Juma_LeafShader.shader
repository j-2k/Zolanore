Shader "Unlit/Juma_LeafShader"
{
    Properties
    {
        _LeafTexture ("Leaf Texture", 2D) = "white" {}
        _NoiseTexture("Noise Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _ColorScale("Color Multiplier", float) = 1
        _NoiseVelocity("Noise Velocity", Vector) = (0.03, 0.03, 0, 0)
        _NoiseVelocity2("Noise Velocity2", Vector) = (0.03, 0.03, 0, 0)
        _ClipAmount("Clip Amount", Range(0, 1)) = 0.9
        _WaveAmp("WaveAmp", Range(-1,1)) = 0.05
        _WaveAmpLeaf("_WaveAmpLeaf", Range(-1,1)) = 0.05
        _DistortionAmount("Distortion Reduction",Range(1,50)) = 1
        _NoiseScale("NoiseScale",float) = 0
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
            // make fog work
            #pragma multi_compile_fog

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
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _LeafTexture;
            float4 _LeafTexture_ST;

            sampler2D _NoiseTexture;
            float4 _NoiseTexture_ST;

            float4 _Color;
            float _ColorScale;

            float2 _NoiseVelocity;
            float2 _NoiseVelocity2;
            
            float _ClipAmount;
            float _WaveAmp;
            float _WaveAmpLeaf;
            float _NoiseScale;
            float _DistortionAmount;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _LeafTexture);
                o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTexture);
                
                float2 noiseUV = float2(o.noiseUV.x + _Time.y * _NoiseVelocity.x,o.noiseUV.y + _Time.y * _NoiseVelocity.y);//normalize(_NoiseVelocity.x) * windspeed
                float waveY = cos((v.uv.y + _Time.y * 0.1) * 6.24 * 5);
                float vertexY = (_WaveAmpLeaf * cos((o.uv.y) + _Time.y * 10));
                o.noiseUV.xy = (noiseUV.xy) * _NoiseScale;
                o.uv.x += vertexY;
                o.vertex.y += waveY * _WaveAmp; //remove1 of waves
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed sampleNoiseRONLY = tex2D(_NoiseTexture, i.noiseUV).r;
                uv = uv + sampleNoiseRONLY * sin(_Time.y * _NoiseVelocity2) / _DistortionAmount;
                fixed4 sampledLeafTexture = tex2D(_LeafTexture, uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                clip(sampledLeafTexture.a - _ClipAmount);
                return sampledLeafTexture * (_Color * _ColorScale);
            }
            ENDCG
        }
    }
}

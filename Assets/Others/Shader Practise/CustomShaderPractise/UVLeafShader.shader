Shader "Unlit/UVLeafShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseVelocity("Noise Velocity", Vector) = (0.03, 0.03, 0, 0)
        _ClipAmount("Clip Amount", Range(0, 1)) = 0.9
        _RotationSpeed("Rot Speed", Range(-20,20)) = 0
        _WaveAmp("WaveAmp", Range(-1,1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
        
        ZWrite On
        //Cull Back
        //Blend One One

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
                float3 normal : NORMAL;
                float2 uv0 : TEXCOORD0;
                //float2 uv1 : TEXCOORD1;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float2 noiseUV : TEXCOORD3;
                //UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            float2 _NoiseVelocity;

            float _ClipAmount;

            float _RotationSpeed;

            float _WaveAmp;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv0, _MainTex);
                o.noiseUV = TRANSFORM_TEX(v.uv0, _NoiseTex);

                /*
                float sinX = sin ( _RotationSpeed * _Time );
                float cosX = cos ( _RotationSpeed * _Time );
                float sinY = sin ( _RotationSpeed * _Time );
                float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
                //v.uv0.xy = mul ( v.uv0.xy, rotationMatrix );
                o.noiseUV = mul( v.uv1.xy, rotationMatrix );
                */

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float2 noiseUV = float2(o.noiseUV.x + _Time.y * _NoiseVelocity.x,o.noiseUV.y + _Time.y * _NoiseVelocity.y);

                //float2 noiseUV = float2(o.noiseUV.x,o.noiseUV.y);
                //float2 offsetUV = 
                //o.vertex.xz += abs(0.1 * cos((worldPos.x) + _Time.y * 1) * 0.5);
                //o.vertex.xz += abs(0.1 * cos((worldPos.x) + _Time.y * 1) * 0.5);
                float waveX = cos((v.uv0.x + _Time.y * 0.1) * 6.24 * 5);
                float waveY = cos((v.uv0.y + _Time.y * 0.1) * 6.24 * 5);
                o.vertex.y += waveX * waveY * _WaveAmp; //remove1 of waves
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //return float4(0.5,0.5,0,1);
                //return float4(i.uv.xy,0,1);
                
                //float2 worldSpaceUV = i.worldPos.xz;
                //float2 noiseUV = float2(i.noiseUV.x + _Time.y * _NoiseVelocity.x,i.noiseUV.y + _Time.y * _NoiseVelocity.y);
                //return float4(i.noiseUV.xy,0,1);

                //float2 worldSpaceUV = i.worldPos.xz;
                //worldSpaceUV += abs(0.1 * cos((worldSpaceUV.xy) + _Time.y * 1) * 0.5);
                // sample the texture
                float2 noiseUV = float2(i.noiseUV.x,i.noiseUV.y);
                fixed4 sampleNoise = tex2D(_NoiseTex, noiseUV);
                //return sampleNoise;
                fixed4 mainTex = tex2D(_MainTex, i.uv);
                //clip(mainTex.a - _ClipAmount);
                return mainTex;

                //return mainTex * sampleNoise;
                


                //clip(col.a);

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //float4 showUV = float4(i.uv.xy,0,1);// i.uv.xy * 2 - 1

                //float4 finalFrag = showUV * sampleNoise;

                //return finalFrag;
                
            }
            ENDCG
        }
    }
}

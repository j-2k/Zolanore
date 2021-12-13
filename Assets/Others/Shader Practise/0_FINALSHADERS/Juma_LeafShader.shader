Shader "Unlit/Juma_LeafShader"
{
    Properties
    {
        _LeafTexture("Leaf Texture", 2D) = "white" {}
        _NoiseTexture("Noise Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _ColorScale("Color Multiplier", float) = 1
        _NoiseVelocity("Noise Velocity", Vector) = (0.5, 0.5, 0, 0)
        _UV_Leaf_Speed("Leaf Wiggle Speed", Vector) = (10, 10, 0, 0)
        _ClipAmount("Clip Amount", Range(0, 1)) = 0.01
        _WaveAmp("Wave Amp Vertex", Range(-1,1)) = 0.05
        _WaveAmpLeaf("Wave Amp Leaf", Range(-1,1)) = 0.01
        _DistortionAmount("Distortion Reduction",Range(1,100)) = 1
        _NoiseScale("NoiseScale",Range(0.01,5)) = 1

    }
        SubShader
        {
            Tags { 
            "RenderType" = "Opaque"
            "LightMode" = "ForwardBase"
            //"LightMode" = "ForwardBase"
            //"PassFlags" = "OnlyDirectional"
            }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                //#pragma multi_compile_fog
                #pragma multi_compile_fwdbase
           
                #include "UnityCG.cginc"
                #include "Lighting.cginc"
                #include "AutoLight.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 _ShadowCoord : TEXCOORD1;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float2 noiseUV : TEXCOORD1;
                    float3 worldUV : TEXCOORD2;
                    //UNITY_FOG_COORDS(1)
                    //SHADOW_COORDS(2)
                };

                sampler2D _LeafTexture;
                float4 _LeafTexture_ST;

                sampler2D _NoiseTexture;
                float4 _NoiseTexture_ST;

                float4 _Color;
                float _ColorScale;

                float2 _NoiseVelocity;
                float2 _UV_Leaf_Speed;

                float _ClipAmount;
                float _WaveAmp;
                float _WaveAmpLeaf;
                float _NoiseScale;
                float _DistortionAmount;

            
                

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _LeafTexture);
                    o.noiseUV = TRANSFORM_TEX(v.uv, _NoiseTexture);

                    //WORLD SPACE LEAF
                    o.worldUV = mul(unity_ObjectToWorld, v.vertex);
                    float2 worldUVScroll = float2(o.worldUV.x + _Time.y * _NoiseVelocity.x,o.worldUV.z + _Time.y * _NoiseVelocity.y);//normalize(_NoiseVelocity.x) * windspeed
                    o.worldUV.xz = (worldUVScroll.xy) * _NoiseScale;

                    //OBJECT SPACE LEAF
                    float2 noiseUV = float2(o.noiseUV.x + _Time.y * _NoiseVelocity.x,o.noiseUV.y + _Time.y * _NoiseVelocity.y);//normalize(_NoiseVelocity.x) * windspeed
                    o.noiseUV.xy = (noiseUV.xy) * _NoiseScale;
                    


                    float waveX = cos((v.uv.x + _Time.y * 0.1) * 6.24 * 5);
                    float vertexY = (_WaveAmpLeaf * cos((o.uv.y) + _Time.y * 10));
                    o.uv.x += vertexY;
                    o.vertex.y += waveX * _WaveAmp; //remove1 of waves
                    //UNITY_TRANSFER_FOG(o,o.vertex);
                    //TRANSFER_SHADOW(o)
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {   
                    //WORLD SPACE LEAF

                    float2 worldUV = i.worldUV.xz;
                    fixed sampleWorldNoiseR = tex2D(_NoiseTexture, worldUV).r;
                    float2 uv = i.uv;
                    uv = uv + sampleWorldNoiseR * sin(_Time.y * _UV_Leaf_Speed) / _DistortionAmount;
                    fixed4 sampLeaf = tex2D(_LeafTexture, uv);
                    clip(sampLeaf.a - _ClipAmount);
                    return sampLeaf;

                    // apply fog
                    //UNITY_APPLY_FOG(i.fogCoord, col);
                    //float shadow = SHADOW_ATTENUATION(i);

                    //OBJECT SPACE LEAF
                    /*
                    float2 uv = i.uv;
                    fixed sampleNoiseRONLY = tex2D(_NoiseTexture, i.noiseUV).r;
                    uv = uv + sampleNoiseRONLY * sin(_Time.y * _UV_Leaf_Speed) / _DistortionAmount;
                    fixed4 sampledLeafTexture = tex2D(_LeafTexture, uv);
                    clip(sampledLeafTexture.a - _ClipAmount);
                    return sampledLeafTexture * (_Color * _ColorScale);
                    */
                }

                /*
                float4 ComputeScreenPos (float4 p)
                {
                    float4 o = p * 0.5;
                    return float4(float2(o.x, o.y*_ProjectionParams.x) + o.w, p.zw);
                }
                

                appdata vert (float4 vertex:POSITION, float2 uv:TEXCOORD0)
                {
                    appdata vs;
                    vs.vertex = UnityObjectToClipPos(vertex);
                    vs.uv = uv;
                    vs._ShadowCoord = ComputeScreenPos(vs.position);
                    return vs;
                }

                float4 frag (appdata ps) : SV_TARGET
                {
                    return lerp(float4(0,0,0,1), tex2D(_LeafTexture, ps.uv), step(0.5, SHADOW_ATTENUATION(ps)));
                }
                */
                
                ENDCG
            }

            Pass
            {
                Tags{ "LightMode" = "ShadowCaster" }
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
    
                float4 vert (float4 vertex:POSITION) : SV_POSITION
                {
                    return UnityObjectToClipPos(vertex);
                }
    
                float4 frag (float4 vertex:SV_POSITION) : SV_TARGET
                {
                    return 0;
                }
            
                ENDCG
            }
            //UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        }
}

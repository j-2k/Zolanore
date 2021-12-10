Shader "Unlit/0_ULPractiseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1("Color1", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (1,1,1,1)
        _ColorSTART("ColorSTART", Range(0,1)) = 0
        _ColorEND("ColorEND", Range(0,1)) = 1
        _Scale ("Scale1", float) = 1
        _OffSet ("OffSet1", float) = 0
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

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uvs : TEXCOORD0;     //THIS IS THE TRUE UV DATA PASSED TO V2F
                float3 normals : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;          //THIS IS JUST A TYPE OF TEX DATA CALLED UV OR THIS IS THE CHANNEL TO SEND DATA THROUGH eg. texcord1234...
                float3 normal : TEXCOORD1;      //example is this tex cord is the normal data  or this tex cord is the uv data its the same thing except how u use it in the vertex shader
                //UNITY_FOG_COORDS(1)

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Scale;
            float _OffSet;
            float4 _Color1;
            float4 _Color2;
            float _ColorSTART;
            float _ColorEND;
            
            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uvs = TRANSFORM_TEX(v.uvs, _MainTex);

                o.normal = UnityObjectToWorldNormal(v.normals);
                //o.uv = (v.uvs + _OffSet) * _Scale;
                o.uv = v.uvs;
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float InverseLerp(float a, float b, float t)
            {
                return (t-a)/(b-a);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uvs);
                
            
            
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);


                //return float4(i.uv.xy,0,1);
                //return float4(i.uv.yyy,1);

                //float t = saturate(InverseLerp(_ColorSTART,_ColorEND,i.uv.y));
                //float t = (InverseLerp(_ColorSTART,_ColorEND,i.uv.y));
                //float t = abs(frac(i.uv.y * 5) * 2 -1);

                float t = sin(i.uv.y * 25) * 0.5 + 0.5;
                return t;

                float4 colorOut = lerp(_Color1,_Color2,t);
                return colorOut;
            }
            ENDCG
        }
    }
}

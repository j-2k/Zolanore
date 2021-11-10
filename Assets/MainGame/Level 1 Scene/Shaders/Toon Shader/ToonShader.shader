Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
            [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque" 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float4 _AmbientColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);


                o.uv = TRANSFORM_TEX(v.uv, _MainTex);


                //UNITY_TRANSFER_FOG(o,o.vertex);


                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 texSample = tex2D(_MainTex, i.uv);
                
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal); //dot 1 to -1 / 1 to 0 lit / 0 to -1 dark
                
                //float lightIntensity = NdotL > 0 ? 1 : 0; //if NdotL > 0 true = 1 else 0
                float lightIntensity = smoothstep(0,0.05f, NdotL);
                float4 light = lightIntensity * _LightColor0;


                return  texSample * _Color * (_AmbientColor + light);
            }
            ENDCG
        }
    }
}

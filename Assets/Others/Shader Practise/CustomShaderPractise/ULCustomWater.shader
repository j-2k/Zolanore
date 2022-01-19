Shader "Unlit/ULCustomWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent"
			"LightMode" = "ForwardBase"
		}
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityStandardUtils.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);


                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float wave1 = sin((v.uv.x + _Time.y * 0.1) * 6.24 * 5);// *_Time.y * 1;
                float wave2 = sin((v.uv.y + _Time.y * 0.1) * 6.24 * 5);// *_Time.y * 1;
                float combinedWaves = wave1 * wave2 * 1;
                o.vertex.y += combinedWaves;
                

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                
                return float4(0.1,0.6,0.9,0.5);
            }
            ENDCG
        }
    }
}

Shader "Unlit/ULShaderPractise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.vertex.y += sin((worldPos.x/5) + _Time.y * 5);
                o.vertex.xy += sin(_Time.y * 5);
                o.uv = v.uv;

                half3x3 m = (half3x3)UNITY_MATRIX_M;
                half3 objectScale = half3(
                    length(half3(m[0][0], m[1][0], m[2][0])),
                    length(half3(m[0][1], m[1][1], m[2][1])),
                    length(half3(m[0][2], m[1][2], m[2][2]))
                    );

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);

                //return col;
                return float4(i.uv.x, i.uv.y,0, 1);
            }
            ENDCG
        }
    }
}

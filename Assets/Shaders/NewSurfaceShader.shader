Shader "Custom/_Skybox"
{
    Properties
    {
        [HDR] _Color("Tint Color", Color) = (.5, .5, .5, 1)
        [NoScaleOffset] _MainTex("Cubemap", Cube) = "grey" {}
    }
 
        SubShader
    {
 
        Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
 
        Pass
        {
 
            Name "Skybox"
 
            CULL FRONT
            ZWRITE OFF
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            samplerCUBE _MainTex;
            half3 _Color;
 
            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 cubeUV : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };
 
            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.cubeUV = v.vertex.xyz;
 
                return o;
            }
 
            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(texCUBE(_MainTex, i.cubeUV).rgb *_Color.rgb, 1);
            }
 
            ENDCG
        }
    }
}
Shader "Custom/VertexColors" {
	Properties {
            _Color ("Color", Color) = (1, 1, 1, 1)
            _MainTex ("Albedo (RGB)", 2D) = "white" {}
            _Glossiness ("Smoothness", Range(0,1)) = 0.5
            _Metallic ("Metallic", Range(0,1)) = 0.0
            _Alpha ("Alpha", Range(0,1)) = 1.0
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 200
            
            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0
    
            sampler2D _MainTex;
    
            struct Input {
                float2 uv_MainTex;
                float4 color : COLOR;
            };
    
            half _Glossiness;
            half _Metallic;
            half _Alpha;
            half4 _Color; 
    
            void surf (Input IN, inout SurfaceOutputStandard o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb * IN.color * _Color.a;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a * _Alpha;
            }
            ENDCG
        }
        FallBack "Diffuse"
}
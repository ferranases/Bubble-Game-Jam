Shader "Custom/WorldSpaceTextureWithShadows" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex_ST ("Texture Scale and Offset", Vector) = (1, 1, 0, 0) // Scale (X, Y) and Offset (X, Y)
        _LightIntensity ("Light Intensity", Range(0, 2)) = 1 // New property to control light intensity
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input {
            float3 worldPos; // World position passed to the surface shader
            float3 normal; // Normal vector passed to the surface shader
        };

        sampler2D _MainTex;
        float4 _MainTex_ST; // Contains scale and offset for the texture
        float4 _Color;
        float _LightIntensity; // Light intensity property

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Calculate UVs based on the world position and normal
            float2 uv = IN.worldPos.xz * _MainTex_ST.xy + _MainTex_ST.zw; // Use XZ plane for UV mapping

            // Rotate UVs 90 degrees to the left
            float2 rotatedUV;
            rotatedUV.x = -uv.y; // Invert the old Y for the new X
            rotatedUV.y = uv.x;  // The new Y is the old X

            // Sample the texture with the rotated UVs
            fixed4 texColor = tex2D(_MainTex, rotatedUV);

            // Modify the albedo based on the light intensity
            o.Albedo = texColor.rgb * _Color.rgb * _LightIntensity;  // Set the surface color modulated by light intensity
            o.Alpha = texColor.a * _Color.a;  // Set the transparency if needed
        }
        ENDCG
    }
    FallBack "Diffuse"
}

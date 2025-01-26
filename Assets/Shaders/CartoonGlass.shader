Shader "Custom/CartoonGlassWithReflectionAndRim"
{
    Properties
    {
        _Color ("Glass Color", Color) = (0.5, 0.7, 1, 0.1)  // Glass color with low alpha for transparency
        _RimColor ("Rim Color", Color) = (0.8, 0.9, 1, 1)   // Rim color for Fresnel effect
        _RimPower ("Rim Power", Range(1.0, 5.0)) = 2.5      // Power for rim effect
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5       // Smoothness of the glass
        _MainTex ("Reflection Texture", 2D) = "white" {}    // Texture for fake reflections
        _ReflectionIntensity ("Reflection Intensity", Range(0, 1)) = 0.5  // Slider to control reflection transparency
        _MainTex_ST ("Tiling and Offset", Vector) = (1,1,0,0)  // Tiling and Offset for the reflection texture

        _RimTex ("Rim Texture", 2D) = "white" {}            // Texture for rim (border) effect
        _RimTexIntensity ("Rim Texture Intensity", Range(0, 1)) = 1.0 // Intensity of the rim texture
        _RimTex_ST ("Rim Tiling and Offset", Vector) = (1,1,0,0) // Tiling and Offset for the rim texture
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        
        Pass
        {
            ZWrite Off         // Disable writing to depth buffer
            Blend SrcAlpha OneMinusSrcAlpha   // Standard transparency blending

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;  // Add texture coordinates
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD0;
                float2 uv : TEXCOORD1;        // Texture coordinates for fake reflection texture
                float2 rimUv : TEXCOORD2;     // Texture coordinates for rim texture
            };

            float4 _Color;
            float4 _RimColor;
            float _RimPower;
            float _Glossiness;
            float _ReflectionIntensity;  // Intensity control for reflection transparency
            sampler2D _MainTex;           // Sampler for fake reflection texture
            float4 _MainTex_ST;           // Tiling and offset control for the reflection texture

            sampler2D _RimTex;            // Sampler for rim texture
            float _RimTexIntensity;       // Intensity control for rim texture
            float4 _RimTex_ST;            // Tiling and offset control for the rim texture

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.vertex).xyz);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);  // Apply tiling and offset to reflection texture UVs
                o.rimUv = TRANSFORM_TEX(v.texcoord, _RimTex); // Apply tiling and offset to rim texture UVs
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Fresnel effect for glass rim/border
                float rim = 1.0 - saturate(dot(i.viewDir, i.worldNormal));
                rim = pow(rim, _RimPower);

                // Make inside of glass transparent
                fixed4 glassColor = _Color;
                glassColor.a *= rim;  // Apply rim transparency only to the borders

                // Sample the fake reflection texture with applied tiling and offset
                fixed4 reflection = tex2D(_MainTex, i.uv);

                // Sample the rim texture with applied tiling and offset
                fixed4 rimTexColor = tex2D(_RimTex, i.rimUv);

                // Blend the reflection texture with the glass color, using _ReflectionIntensity to control transparency
                reflection.a *= _ReflectionIntensity;  // Control reflection transparency
                fixed4 finalColor = glassColor + reflection * reflection.a;

                // Add the rim texture only to the rim effect area, controlled by the Fresnel rim factor
                finalColor += rimTexColor * rim * _RimTexIntensity;

                return finalColor + _RimColor * rim;  // Combine glass, reflections, rim light, and rim texture
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}

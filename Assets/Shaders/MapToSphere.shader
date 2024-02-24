Shader "Custom/MapToSphere"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 direction;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.direction = v.normal;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            const float PI = 3.14159265359;
            // Puff out the direction to compensate for interpolation.
            float3 direction = normalize(IN.direction);
        
            // Get a longitude wrapping eastward from x-, in the range 0-1.
            float longitude = 0.5 - atan2(direction.z, direction.x) / (2.0f * PI);
            // Get a latitude wrapping northward from y-, in the range 0-1.
            float latitude = 0.5 + asin(direction.y) / PI;
        
            // Combine these into our own sampling coordinate pair.
            float2 customUV = float2(longitude, latitude);
        
            // Use them to sample our texture(s), instead of the defaults.
            fixed4 c = tex2D (_MainTex, customUV) * _Color;
        
            // Albedo comes from a texture tinted by color
            // fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

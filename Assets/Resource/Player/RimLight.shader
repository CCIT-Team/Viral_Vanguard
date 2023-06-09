Shader "Custom/RimLight"
{
   Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimPower("RimPower" , Range(0,10)) = 0 
        _NormalMap ("NormalMap",2D ) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
 
        CGPROGRAM
        #pragma surface surf Lambert noambient
        #pragma target 3.0
 
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
 
            float3 viewDir;
        };
   
        fixed4 _Color;
        float _RimPower;
        sampler2D _MainTex;
        sampler2D _NormalMap;
 
    
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
 
            o.Normal = UnpackNormal(tex2D (_NormalMap, IN.uv_NormalMap));// //
 
            float rim  = saturate(dot(o.Normal , IN.viewDir));
            rim = pow ( 1-rim , _RimPower);
            o.Emission = rim * _Color.rgb;
 
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
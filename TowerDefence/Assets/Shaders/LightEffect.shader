// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D/Texture Blend2"
 {  
     Properties
     {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Alpha Color Key", Color) = (0,0,0,1)
        _AdditionalColor ("+Color Key", Color) = (0,0,0,1)
        _Brightness ("Brightness color (gray default)", Color) = (0,0,0,1)
        _Contrast ("Contrast", float) = 1
        _PowerOfMult ("Iterator", int) = 1
        [Toggle ]_ChangeColor ("ChangeColor", float) = 0
     }
     SubShader
     {
         Tags 
         { 
             "RenderType" = "Transparent" 
             "Queue" = "Transparent" 
             "IgnoreProjector"="True"
         }
 
         Pass
         {
             Lighting On
             ZWrite Off
             Cull Off
             Blend SrcAlpha OneMinusSrcAlpha
             //Cull front 
             LOD 100
  
             CGPROGRAM
             #pragma vertex vert alpha
             #pragma fragment frag alpha
             #include "UnityCG.cginc"
  
             sampler2D _MainTex;
             float4 _Color;
             float4 _Brightness;
             float4 _AdditionalColor;
             float _Contrast;
             int _PowerOfMult;
             float _ChangeColor;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uv2 : TEXCOORD1;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.uv2 = v.uv2;
     
                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 float4 o = float4(1, 0, 0, 0);
 
                 half4 c = tex2D (_MainTex, IN.uv_MainTex);

                 if (_ChangeColor) {
                    o.rgb = _Brightness;
                 } else {
                    o.rgb = c.rgb + _Brightness;
                 }

                 float factor = (259 * (_Contrast + 255)) / (255 * (259 - _Contrast));

                 for (int i = 0; i < _PowerOfMult; i++) {
                    o.r *= o.r;
                    o.g *= o.g;
                    o.b *= o.b;
                 }

                 o.r = factor * (o.r - 128) + 128;
                 o.g = factor * (o.g - 128) + 128;
                 o.b = factor * (o.b - 128) + 128;
                     
                 o.a = c.a;
                 o.a -= _Color.a;
                 if (o.a < 0)
                 o.a = 0;
                 return o;
             }
 
             ENDCG
         }
     }
 }
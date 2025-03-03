Shader "Custom/BorderShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,0.4)
        _BorderThickness("Border Thickness", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _BorderThickness;
            fixed4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float border = _BorderThickness;
                if (i.uv.x < border || i.uv.x > 1 - border || i.uv.y < border || i.uv.y > 1 - border)
                {
                    return _Color; // 边框颜色
                }
                return fixed4(0, 0, 0, 0); // 透明背景
            }
            ENDCG
        }
    }
}

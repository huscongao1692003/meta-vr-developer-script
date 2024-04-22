Shader "Softspace/FastRing" {
	Properties {
		_LineColor ("Line Color", Vector) = (1,1,1,1)
		_LineThickness ("Line Thickness", Range(0.001, 0.2)) = 0.01
		[Space] _Origin ("Origin", Vector) = (0,0,0,1)
		_Radius ("Radius", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}
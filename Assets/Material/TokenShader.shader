Shader "Example/Diffuse Simple" {
	Properties{
		_Color ("Color(RGB)", color) = (1,1,1,1)

	}

      SubShader {

    	ztest false
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert

      half3 _Color;

      struct Input {
          float4 color : COLOR;
      };
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _Color.rgb;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }
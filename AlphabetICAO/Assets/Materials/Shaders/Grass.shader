Shader "Custom/Grass" 
{
    Properties {
        _myTex ("Diffuse Texture", 2D) = "white" {}
		_myNormal ("Normal Texture", 2D) = "bump" {}
		_mySlider("Normal Intensity", Range(0,10)) = 1

    }
    SubShader {

      CGPROGRAM
        #pragma surface surf Lambert
        
        sampler2D _myTex;
		sampler2D _myNormal;
		half _mySlider;

        struct Input {
            float2 uv_myTex;
			float2 uv_myNormal;

        };
        
        void surf (Input IN, inout SurfaceOutput o) 
		{
            o.Albedo = tex2D(_myTex, IN.uv_myTex).rgb;
			o.Normal = UnpackNormal(tex2D(_myNormal, IN.uv_myNormal));
			o.Normal *= float3(_mySlider, _mySlider, 1);
        }      
      ENDCG
    }
    Fallback "Diffuse"
  }
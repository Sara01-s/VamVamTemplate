Shader "Custom/sh_transition" {

    Properties {
        _MainTex ("Texture", 2D) = "white" {}
		_Progress ("Progress", Float) = 0.0

		[Toggle(INVERT)]
		_Invert ("Invert", Float) = 0

		[Space(20)]
		_ShapeSize ("Shape Size", Range(0.0, 1.0)) = 0.0
    }

    SubShader {
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex VertShader
            #pragma fragment FragShader
			#pragma shader_feature INVERT

            #include "UnityCG.cginc"

            struct VertexIn {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOut {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            VertexOut VertShader (VertexIn vertIn) {
                VertexOut vertOut;

                vertOut.pos = UnityObjectToClipPos(vertIn.pos);
                vertOut.uv = vertIn.uv;

                return vertOut;
            }

            sampler2D _MainTex;
			float _Progress;
			float _ShapeSize;

			float Remap(float value, float oldMin, float oldMax, float newMin, float newMax) {
				return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
			}

            fixed4 FragShader (VertexOut i) : SV_Target {

				_Progress = Remap(_Progress, 0, 1, -1, 2); // zzz...

				float xFrac = frac(i.uv.x / _ShapeSize);
				float yFrac = frac(i.uv.y / _ShapeSize);

				float xDst = abs(xFrac - 0.5);
				float yDst = abs(yFrac - 0.5);
				
#if INVERT
				if ((xDst + yDst) + (i.uv.x + -i.uv.y) < _Progress) {
					discard;
				}
#else
				if ((xDst + yDst) + (i.uv.x + -i.uv.y) > _Progress) {
					discard;
				}
#endif

                return fixed4(0.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}

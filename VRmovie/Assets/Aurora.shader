Shader "Custom/Aurora" {
	Properties{
		_Color("Color" , Color)=(1,1,1,1)
		_MainTex("Aurora", 2D) = "white" {}
		_ScrollSpeed("ScrollSpeed" , float)=1
	}
		SubShader{
			Tags { 
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			LOD 200
			
			CGPROGRAM
			#pragma surface surf Standard alpha:fade Lambert vertex:vert
			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;
			float _ScrollSpeed;
			struct Input {
				float2 uv_MainTex;
			};
			void vert(inout appdata_full v, out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input, o);
				float amp = 1.5*sin(_Time * 100 + v.vertex.x * 100);
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
				//v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
			}
			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed2 uv = IN.uv_MainTex;
				uv.x += _ScrollSpeed * _Time;
				fixed4 c2 = tex2D(_MainTex, uv)*_Color;
				o.Albedo = c2.rgb;
				o.Alpha = c2.a;
			}
			ENDCG
	}
		FallBack "Diffuse"
}

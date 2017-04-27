// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "P&D/TorchLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Glow("Light Intensity",Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent"   "Queue" = "Transparent+1"}

		Pass
		{
			BlendOp Add , Add
			Blend Zero One, DstAlpha Zero
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float _Glow;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex =UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = pow(col.a,_Glow);
				return col;
			}
			ENDCG
		}
	}
}

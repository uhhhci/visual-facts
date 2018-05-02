﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/DistanceColor"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Distance("Distance", Float) = 0
		_CamPos("CamPosistion",Vector) = (0, 0, 10, 0)
		_ColorShader("Color",Vector) = (0,0,1,1)
		_Size("ObjectSize",Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 col : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Distance;
			float4 _CamPos;
			float4 _ColorShader;
			float _Size;
			
			v2f vert (appdata v)
			{
				v2f o;
				
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float4 camPos_rel = mul(worldPos, _CamPos);
				if (camPos_rel.z / _Size * 42 > _Distance )
					o.col = _ColorShader;
				else
					o.col = float4(0, 0, 0, 0);

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col = i.col;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

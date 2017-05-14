Shader "Custom/toon/simple"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}

		[Toggle(RIM_ON)] _Rim ("Rim", Float) = 1.0
		_RimPower("Silhouette Strength", Range(0.0, 20.0)) = 10.0
	}

	SubShader
	{
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma multi_compile _ SHADOWS_SCREEN
			#pragma multi_compile _ RIM_ON

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_ST;
			uniform float _RimPower;

			uniform half4 _LightColor0;

			struct appdata {
				float4 vertex : POSITION;
				half3 normal  : NORMAL;
				fixed2 coord  : TEXCOORD0;
			};

			struct v2f {
				float4 pos        : SV_POSITION;
				fixed2 texcoords  : TEXCOORD0;
				float4 posWorld   : TEXCOORD1;
				half3 normalWorld : TEXCOORD2;
				
				#if defined(SHADOWS_SCREEN)
				SHADOW_COORDS(3)
				#endif	
			};


			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoords = v.coord;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalWorld = normalize(mul(half4(v.normal, 0.0), unity_WorldToObject).xyz);

				#if defined(SHADOWS_SCREEN)
				TRANSFER_SHADOW(o);
				#endif
				
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				float  a;
				half3 l;

				if (_WorldSpaceLightPos0.w == 0.0)
				{
					l = normalize(_WorldSpaceLightPos0.xyz);
					a = 1.0;
				}
				else
				{
					l = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					a = 1.0 / length(l);
					l = normalize(l);
				}

				half3 n = normalize(i.normalWorld);
				half3 v = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);

				half4 tex = tex2D(_MainTex, i.texcoords.xy * _MainTex_ST.xy + _MainTex_ST.zw);

				half rim = 1.0;
				
				#ifdef RIM_ON
				rim = pow(saturate(dot(n, v)), _RimPower);
				rim = pow(rim, 0.4);
				rim += (rim > 0.4);

				#endif

				// Color
				float colorIntensity;

				float ndotl = a * (0.5 + 0.5 * dot(n, l));

				if (ndotl <= 0.0) { colorIntensity = 0.0; }
				else if (ndotl > 0.0 && ndotl <= 0.4) { colorIntensity = 0.1; }
				else if (ndotl > 0.4 && ndotl <= 0.6) { colorIntensity = 0.3; }
				else if (ndotl > 0.6 && ndotl <= 0.9) { colorIntensity = 0.6; }
				else { colorIntensity = 1.0; }
				
				float shadowAttenuation = 1.0;

				#if defined(SHADOWS_SCREEN)
				shadowAttenuation = SHADOW_ATTENUATION(i);
				#endif

				half3 finalLight = colorIntensity * _LightColor0.rgb * tex.rgb;

				return rim * finalLight.rgbb * shadowAttenuation;
			}
			
			ENDCG
		}
		
		Pass
		{
			Tags{ "LightMode" = "ForwardAdd" }
			
			Blend One One

			CGPROGRAM

			#pragma multi_compile_fwdadd_fullshadows
			#pragma multi_compile _ RIM_ON

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_ST;
			uniform float _RimPower;

			uniform half4 _LightColor0;

			struct appdata {
				float4 vertex : POSITION;
				half3 normal  : NORMAL;
				fixed2 coord  : TEXCOORD0;
			};

			struct v2f {
				float4 pos        : SV_POSITION;
				fixed2 texcoords  : TEXCOORD0;
				float4 posWorld   : TEXCOORD1;
				half3 normalWorld : TEXCOORD2;
				
				#if defined(SHADOWS_DEPTH) && defined(SPOT) || defined(SHADOWS_CUBE) && defined(POINT)
				SHADOW_COORDS(3)
				#endif
			};


			v2f vert(appdata v)
			{
				v2f o;
				o.pos         = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoords   = v.coord;
				o.posWorld    = mul(unity_ObjectToWorld, v.vertex);
				o.normalWorld = normalize(mul(half4(v.normal, 0.0), unity_WorldToObject).xyz);


				#if defined(SHADOWS_DEPTH) && defined(SPOT) || defined(SHADOWS_CUBE) && defined(POINT)
				TRANSFER_SHADOW(o);
				#endif

				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				float  a;
				half3 l;

				if (_WorldSpaceLightPos0.w == 0.0)
				{
					l = normalize(_WorldSpaceLightPos0.xyz);
					a = 1.0;
				}
				else
				{
					l = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
					a = saturate(3.0 / length(l));
					l = normalize(l);
				}

				half3 n = normalize(i.normalWorld);
				half3 v = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);

				half4 tex = tex2D(_MainTex, i.texcoords.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				
				half rim = 1.0;

				#ifdef RIM_ON
				rim = pow(saturate(dot(n, v)), _RimPower);
				rim = pow(rim, 0.4);
				rim += (rim > 0.4);
				#endif

				// Color
				float colorIntensity;

				float ndotl = a * (0.5 + 0.5 * dot(n, l));

				if (ndotl <= 0.0) { colorIntensity = 0.05; }
				else if (ndotl > 0.0 && ndotl <= 0.3) { colorIntensity = 0.1; }
				else if (ndotl > 0.3 && ndotl <= 0.7) { colorIntensity = 0.3; }
				else { colorIntensity = 1.0; }

				float shadowAttenuation = 1.0;

				#if defined(SHADOWS_DEPTH) && defined(SPOT) || defined(SHADOWS_CUBE) && defined(POINT)
				shadowAttenuation = SHADOW_ATTENUATION(i);
				#endif

				half3 finalLight = colorIntensity * _LightColor0.rgb * tex.rgb;

				return rim * finalLight.rgbb * shadowAttenuation;
			}

			ENDCG
		}
	
		Pass
		{
			Tags{ "LightMode" = "ShadowCaster" }

			CGPROGRAM

			#pragma target 3.0 // TODO: Si lo quito en principio no pasa nada

			#pragma multi_compile_shadowcaster
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			struct appdata {
				float4 vertex  : POSITION;
				half3 normal : NORMAL;
			};

			#if defined(SHADOWS_CUBE)

			struct v2f {
				float4 pos : SV_POSITION;
				half3 light : TEXCOORD0;
			};

			v2f vert(appdata i)
			{
				v2f o;
				o.pos    = UnityObjectToClipPos(i.vertex.xyz);
				o.light  = mul(unity_ObjectToWorld, i.vertex).xyz - _LightPositionRange.xyz;

				return o;
			}

			half4 frag(v2f i) : SV_TARGET
			{
				float depth = length(i.light) + unity_LightShadowBias.x;
				
				depth *= _LightPositionRange.w;

				return UnityEncodeCubeShadowDepth(depth);
			}

			#else // !SHADOWS CUBE

			float4 vert(appdata i) : SV_POSITION
			{
				float4 pos = UnityClipSpaceShadowCasterPos(i.vertex.xyz, i.normal);
				return UnityApplyLinearShadowBias(pos);
			}

			half4 frag() : SV_TARGET
			{
				return 0;
			}

			#endif

			ENDCG
		}
		
	}
}
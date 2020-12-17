void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color,
	out float DistanceAttenuation, out float ShadowAttenuation)
{
#if SHADERGRAPH_PREVIEW
	Direction = float3(0.5, 0.5, 0);
	Color = 1;
	DistanceAttenuation = 1;
	ShadowAttenuation = 1;
#else
	Light mainLight = GetMainLight();
	Direction = mainLight.direction;
	Color = mainLight.color;
	DistanceAttenuation = mainLight.distanceAttenuation;

	float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
	ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
	half shadowStrength = GetMainLightShadowStrength();
	ShadowAttenuation = SampleShadowmap(shadowCoord, TEXTURE2D_ARGS(_MainLightShadowmapTexture,
		sampler_MainLightShadowmapTexture), shadowSamplingData, shadowStrength, false);
#endif
}

void DirectSpecular_float(float3 Specular, float Smoothness, float3 Direction, float3 Color, float3 WorldNormal, float3 WorldView, out float3 Out)
{
#if SHADERGRAPH_PREVIEW
	Out = 0;
#else
	Smoothness = exp2(10 * Smoothness + 1);
	WorldNormal = normalize(WorldNormal);
	WorldView = SafeNormalize(WorldView);
	Out = LightingSpecular(Color, Direction, WorldNormal, WorldView, float4(Specular, 0), Smoothness);
#endif
}


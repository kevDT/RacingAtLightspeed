#ifndef SOBELOUTLINES_INCLUDED
#define SOBELOUTLINES_INCLUDED

//Pointsto sample relative to central pixel
static float2 sobelSamplePoints[9] = {
	float2(-1,1), float2(0,1), float2(1,1),
	float2(-1,0), float2(0,0), float2(1,1),
	float2(-1,-1), float2(0,-1), float2(1,-1)
};

//Weights for x direction
static float sobelXMatrix[9] = {
	1,0,-1,
	2,0,-2,
	1,0,-1
};

//Weights for y direction
static float sobelYMatrix[9] = {
	1,2,1,
	0,0,0,
	-1,-2,-1
};

void DepthSobel_float(float2 uv, float thickness, out float returnValue)
{
	float2 sobel = 0;
	[unroll] for (int i = 0; i < 9; i++)
	{
		float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv + sobelSamplePoints[i] * thickness);
		sobel += depth * float2(sobelXMatrix[i], sobelYMatrix[i]);

		returnValue = length(sobel);
	}
}

//void ColorSobel_float(float2 uv, float thickness, out float returnValue)
//{
//	float2 sobelR = 0;
//	float2 sobelG = 0;
//	float2 sobelB = 0;
//
//	[unroll] for (int i = 0; i < 9; i++)
//	{
//		float3 rgb = SHADERGRAPH_SAMPLE_SCENE_COLOR(uv + thickness + sobelSamplePoints[i]);
//
//		float2 kernel = float2(sobelXMatrix[i], sobelYMatrix[i]);
//
//		sobelR += rgb.r * kernel;
//		sobelG += rgb.g * kernel;
//		sobelB += rgb.b * kernel;
//	}
//
//	returnValue = max(length(sobelR), max(length(sobelG, sobelB)));
//}

#endif
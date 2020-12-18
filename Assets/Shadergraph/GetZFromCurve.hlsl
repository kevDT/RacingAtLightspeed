void GetZFromCurve_float(float3 param, float shift, float za, float zb, float zc, out float returnValue)
{
	float top = param.x * za * exp(float(-(pow((param.y * shift) - zb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(zc, 2)))))) * sqrt(float(float(2) * (float)3.14159265358979323));
	float bottom = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / pow(zc, 2)));

	returnValue = top / bottom;
}
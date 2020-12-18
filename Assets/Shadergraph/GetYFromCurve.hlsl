void GetYFromCurve_float(float3 param, float shift, float ya, float yb, float yc, out float returnValue)
{
	float top = param.x * ya * exp(float(-(pow((param.y * shift) - yb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(yc, 2)))))) * sqrt(float(float(2) * (float)3.14159265358979323));
	float bottom = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / pow(yc, 2)));

	returnValue = top / bottom;
}
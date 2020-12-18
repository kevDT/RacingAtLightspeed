void GetXFromCurve_float(float3 param, float shift, float xla, float xlb, float xlc, float xha, float xhb, float xhc, out float returnValue)
{
	float top1 = param.x * xla * exp((float)(-(pow((param.y * shift) - xlb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(xlc, 2)))))) * sqrt((float)(float(2) * (float)3.14159265358979323));
	float bottom1 = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / pow(xlc, 2)));

	float top2 = param.x * xha * exp(float(-(pow((param.y * shift) - xhb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(xhc, 2)))))) * sqrt((float)(float(2) * (float)3.14159265358979323));
	float bottom2 = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / pow(xhc, 2)));

	returnValue = (top1 / bottom1) + (top2 / bottom2);
}
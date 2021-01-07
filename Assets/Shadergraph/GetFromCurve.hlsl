static const float xla = 0.3995281;
static const float xlb = 444.6316;
static const float xlc = 20.09546;
static const float xha = 1.130558;
static const float xhb = 593.2311;
static const float xhc = 34.44604;

static const float ya = 1.009887;
static const float yb = 556.0372;
static const float yc = 46.18487;

static const float za = 2.06484;
static const float zb = 448.4513;
static const float zc = 22.3573;

static const float pi = 3.14159265358979323;

void GetXFromCurve_float(float3 param, float shift, out float returnValue)
{
	float top1 = param.x * xla * exp((float)(-(pow((param.y * shift) - xlb, 2)
		/ (2 * (pow(param.z * shift, 2) + (xlc * xlc)))))) * sqrt((2 * pi));
	float bottom1 = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / (xlc * xlc)));

	float top2 = param.x * xha * exp(float(-(pow((param.y * shift) - xhb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(xhc, 2)))))) * sqrt((2 * pi));
	float bottom2 = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / (xhc * xhc)));

	returnValue = (top1 / bottom1) + (top2 / bottom2);
}

void GetYFromCurve_float(float3 param, float shift, out float returnValue)
{
	float top = param.x * ya * exp(float(-(pow((param.y * shift) - yb, 2)
		/ (2 * (pow(param.z * shift, 2) + yc * yc))))) * sqrt(2 * pi);
	float bottom = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / (yc * yc)));

	returnValue = top / bottom;
}

void GetZFromCurve_float(float3 param, float shift, out float returnValue)
{
	float top = param.x * za * exp(float(-(pow((param.y * shift) - zb, 2)
		/ (2 * (pow(param.z * shift, 2) + pow(zc, 2)))))) * sqrt(2 * pi);
	float bottom = sqrt((float)(1 / pow(param.z * shift, 2)) + (1 / (zc * zc)));

	returnValue = top / bottom;
}
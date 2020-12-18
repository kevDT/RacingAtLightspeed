void ConstrainRGB_float(float3 rgb, out float3 rgbFinal)
{
	float w;

	w = (0 < rgb.r) ? 0 : rgb.r;
	w = (w < rgb.g) ? w : rgb.g;
	w = (w < rgb.b) ? w : rgb.b;
	w = -w;

	if (w > 0) {
		rgb.r += w;  rgb.g += w; rgb.b += w;
	}
	w = rgb.r;
	w = (w < rgb.g) ? rgb.g : w;
	w = (w < rgb.b) ? rgb.b : w;

	if (w > 1)
	{
		rgb.r /= w;
		rgb.g /= w;
		rgb.b /= w;
	}

	rgbFinal = rgb;
}
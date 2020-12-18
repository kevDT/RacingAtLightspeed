void XYZToRGB_float(float3 xyz, out float3 rgb)
{
	rgb.x = 9.94845 * xyz.x - 5.1485 * xyz.y - 1.16389 * xyz.z;
	rgb.y = -2.86007 * xyz.x + 5.77745 * xyz.y - 0.0179627 * xyz.z;
	rgb.z = 0.000174791 * xyz.x - 0.000353084 * xyz.y + 2.79113 * xyz.z;
}
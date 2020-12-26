void RGBToXYZ_float(float3 rgb, out float3 xyz)
{
	xyz.x = 0.13514 * rgb.x + 0.120432 * rgb.y + 0.057128 * rgb.z;
	xyz.y = 0.0668999 * rgb.x + 0.232706 * rgb.y + 0.0293946 * rgb.z;
	xyz.z = 0.0 * rgb.x + 0.0000218959 * rgb.y + 0.358278 * rgb.z;
}
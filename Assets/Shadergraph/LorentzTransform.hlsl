void CalculateNewPos_float(float3 offsettedPos, float relativeSpeed, float speed, float3 playerVel, float3 objectVel, out float3 finalPos)
{
	if (relativeSpeed != 0)
	{
		float a;
		float ux;
		float uy;
		float ca;
		float sa;

		if (speed != 0)
		{
			a = -acos(-playerVel.z / speed);
			if (playerVel.x != 0 || playerVel.y != 0)
			{
				float xyMagnitude = sqrt(pow(playerVel.x, 2) + pow(playerVel.y, 2));
				ux = playerVel.y / xyMagnitude;
				uy = -playerVel.x / xyMagnitude;
			}
			else
			{
				ux = 0.0f;
				uy = 0.0f;
			}


			ca = cos(a);
			sa = sin(a);

			//Rotate player velocity
			//All equations are based of movement in one direction

			float3 originalPos = offsettedPos;
			offsettedPos.x = originalPos.x * (ca + ux * ux * (1 - ca)) + originalPos.y * (ux * uy * (1 - ca)) + originalPos.z * (uy * sa);
			offsettedPos.y = originalPos.x * (uy * ux * (1 - ca)) + originalPos.y * (ca + uy * uy * (1 - ca)) - originalPos.z * (ux * sa);
			offsettedPos.z = originalPos.x * (-uy * sa) + originalPos.y * (ux * sa) + originalPos.z * (ca);
		}

		float3 rotateObjVel = float3(0, 0, 0);
		float spdOfLight = 20;
		if (speed != 0)
		{
			//Rotate object velocity => similar to player velocity
			rotateObjVel.x = (objectVel.x * (ca + ux * ux * (1 - ca)) + objectVel.y * (ux * uy * (1 - ca)) + objectVel.z * (uy * sa)) * spdOfLight;
			rotateObjVel.y = (objectVel.x * (uy * ux * (1 - ca)) + objectVel.y * (ca + uy * uy * (1 - ca)) - objectVel.z * (ux * sa)) * spdOfLight;
			rotateObjVel.z = (objectVel.x * (-uy * sa) + objectVel.y * (ux * sa) + objectVel.z * (ca)) * spdOfLight;
		}
		else
		{
			rotateObjVel.x = (objectVel.x) * spdOfLight;
			rotateObjVel.z = (objectVel.z) * spdOfLight;
			rotateObjVel.y = (objectVel.y) * spdOfLight;
		}

		float c = -(offsettedPos.x * offsettedPos.x + offsettedPos.y * offsettedPos.y + offsettedPos.z * offsettedPos.z); 

		float b = -(2 * (offsettedPos.x * rotateObjVel.x + offsettedPos.y * rotateObjVel.y + offsettedPos.z * rotateObjVel.z));

		float d = (spdOfLight * spdOfLight) - (rotateObjVel.x * rotateObjVel.x + rotateObjVel.y * rotateObjVel.y + rotateObjVel.z * rotateObjVel.z);

		float tisw = (float)(((-b - (sqrt((b * b) - ((float)float(4)) * d * c))) / (((float)float(2)) * d)));

		offsettedPos.x += rotateObjVel.x * tisw;
		offsettedPos.y += rotateObjVel.y * tisw;
		offsettedPos.z += rotateObjVel.z * tisw;

		//Apply Lorentz transform
		float newz = (((float)speed * spdOfLight) * tisw);

		newz = offsettedPos.z + newz;
		newz /= (float)sqrt(1 - (speed * speed));
		offsettedPos.z = newz;
		if (speed != 0)
		{
			float trx = offsettedPos.x;
			float trry = offsettedPos.y;

			offsettedPos.x = offsettedPos.x * (ca + ux * ux * (1 - ca)) + offsettedPos.y * (ux * uy * (1 - ca)) - offsettedPos.z * (uy * sa);
			offsettedPos.y = trx * (uy * ux * (1 - ca)) + offsettedPos.y * (ca + uy * uy * (1 - ca)) + offsettedPos.z * (ux * sa);
			offsettedPos.z = trx * (uy * sa) - trry * (ux * sa) + offsettedPos.z * (ca);
		}
	}

	finalPos = offsettedPos;
}


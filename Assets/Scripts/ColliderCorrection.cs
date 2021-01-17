using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderCorrection : MonoBehaviour
{
    private Movement _objectMovement = null;

    private BoxCollider _collider = null;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _objectMovement = transform.root.GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        Vector3 newPos = CalculateOffsettedPos();
        CorrectPhysics(newPos);
    }

    private void CorrectPhysics(Vector3 pos)
    {
        _collider.center = Vector3.zero;
        _collider.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        _collider.center = pos - transform.position;
    }

    private Vector3 CalculateOffsettedPos()
    {
        //Parameters needed in calculations
        //Variable to change, must get updated every frame
        Vector3 playerVelocity = GameState.Instance.PlayerVelocity / GameState.LIGHTSPEED;
        Vector3 playerPos = GameState.Instance.PlayerPosition;
        Vector3 objectVelocity = Vector3.zero;

        Vector3 offsettedPos = transform.position - playerPos;

        if (_objectMovement)
            objectVelocity = _objectMovement.ObjectVelocity / GameState.LIGHTSPEED;

        bool relativeVelZero = objectVelocity == playerVelocity; //Here, speed is not relevant, nor is the direction

        float speed = playerVelocity.magnitude;

        bool zeroSpeed = Mathf.Approximately(speed, 0);

        //Apply Lorentz Transform
        if (!relativeVelZero)
        {
            float a = 0;
            float ux = 0;
            float uy = 0;
            float ca = 0;
            float sa = 0;

            if (!zeroSpeed)
            {
                a = -Mathf.Acos(-playerVelocity.z / speed);
                if (playerVelocity.x != 0 || playerVelocity.y != 0)
                {
                    float xyMagnitude = Mathf.Sqrt(Mathf.Pow(playerVelocity.x, 2) + Mathf.Pow(playerVelocity.y, 2));
                    ux = playerVelocity.y / xyMagnitude;
                    uy = -playerVelocity.x / xyMagnitude;
                }
                else
                {
                    ux = 0.0f;
                    uy = 0.0f;
                }


                ca = Mathf.Cos(a);
                sa = Mathf.Sin(a);

                //Rotate player velocity
                //All equations are based of movement in one direction

                Vector3 originalPos = offsettedPos;
                offsettedPos.x = originalPos.x * (ca + ux * ux * (1 - ca)) + originalPos.y * (ux * uy * (1 - ca)) + originalPos.z * (uy * sa);
                offsettedPos.y = originalPos.x * (uy * ux * (1 - ca)) + originalPos.y * (ca + uy * uy * (1 - ca)) - originalPos.z * (ux * sa);
                offsettedPos.z = originalPos.x * (-uy * sa) + originalPos.y * (ux * sa) + originalPos.z * (ca);
            }

            Vector3 rotateObjVel = Vector3.zero;
            float spdOfLight = GameState.LIGHTSPEED;
            if (!zeroSpeed)
            {
                //Rotate object velocity => similar to player velocity
                rotateObjVel.x = (objectVelocity.x * (ca + ux * ux * (1 - ca)) + objectVelocity.y * (ux * uy * (1 - ca)) + objectVelocity.z * (uy * sa)) * spdOfLight;
                rotateObjVel.y = (objectVelocity.x * (uy * ux * (1 - ca)) + objectVelocity.y * (ca + uy * uy * (1 - ca)) - objectVelocity.z * (ux * sa)) * spdOfLight;
                rotateObjVel.z = (objectVelocity.x * (-uy * sa) + objectVelocity.y * (ux * sa) + objectVelocity.z * (ca)) * spdOfLight;
            }
            else
            {
                rotateObjVel.x = (objectVelocity.x) * spdOfLight;
                rotateObjVel.z = (objectVelocity.z) * spdOfLight;
                rotateObjVel.y = (objectVelocity.y) * spdOfLight;
            }

            float c = -(offsettedPos.x * offsettedPos.x + offsettedPos.y * offsettedPos.y + offsettedPos.z * offsettedPos.z);

            float b = -(2 * (offsettedPos.x * rotateObjVel.x + offsettedPos.y * rotateObjVel.y + offsettedPos.z * rotateObjVel.z));

            float d = (spdOfLight * spdOfLight) - (rotateObjVel.x * rotateObjVel.x + rotateObjVel.y * rotateObjVel.y + rotateObjVel.z * rotateObjVel.z);

            float tisw = (-b - (Mathf.Sqrt((b * b) - (4 * d * c)))) / (2 * d);

            offsettedPos.x += rotateObjVel.x * tisw;
            offsettedPos.y += rotateObjVel.y * tisw;
            offsettedPos.z += rotateObjVel.z * tisw;

            //Apply Lorentz transform
            float newz = ((speed * spdOfLight) * tisw);

            newz = offsettedPos.z + newz;
            newz /= Mathf.Sqrt(1 - (speed * speed));
            offsettedPos.z = newz;
            if (!zeroSpeed)
            {
                float trx = offsettedPos.x;
                float trry = offsettedPos.y;

                offsettedPos.x = offsettedPos.x * (ca + ux * ux * (1 - ca)) + offsettedPos.y * (ux * uy * (1 - ca)) - offsettedPos.z * (uy * sa);
                offsettedPos.y = trx * (uy * ux * (1 - ca)) + offsettedPos.y * (ca + uy * uy * (1 - ca)) + offsettedPos.z * (ux * sa);
                offsettedPos.z = trx * (uy * sa) - trry * (ux * sa) + offsettedPos.z * (ca);
            }
        }

        offsettedPos += playerPos;

        return offsettedPos;
    }
}

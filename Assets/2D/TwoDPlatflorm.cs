using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlatflorm : MonoBehaviour
{
    public Vector3 platformPostion;
    public float platformRotation;



    Vector3 a1 = new Vector3(-2, 0, 0);
    Vector3 a2 = new Vector3(2, 0, 0);

    Vector3 b1 = new Vector3(-1, 0, 0);
    Vector3 b2 = new Vector3(1, 0, 0);

    Vector3 s1;
    Vector3 s2;

    public float leg1;
    public float leg2;

    bool draw = false;

    private void Update()

    {  
        s1 = platformPostion + ZRotationMatrix(platformRotation * Mathf.Deg2Rad, b1) - a1;
        s2 = platformPostion + ZRotationMatrix(platformRotation * Mathf.Deg2Rad, b2) - a2;

        leg1 = Vector3.Distance(a1, a1 + s1);
        leg2 = Vector3.Distance(a2, a2 + s2);

        


        draw = true;
    }
    Vector3 ZRotationMatrix(float _platformRotation,Vector3 _b )
    {
        float x = (Mathf.Cos(_platformRotation) * _b.x) + (-Mathf.Sin(_platformRotation) * _b.y);
        float y = (Mathf.Sin(_platformRotation) * _b.x) + (Mathf.Cos(_platformRotation) * _b.y);
        float z = _platformRotation;

        return new Vector3(x, y, z);
    }
    private void OnDrawGizmos()
    {
        if (draw)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(a1, a1+s1);
            Gizmos.DrawLine(a2, a2+s2);

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(a1+s1, platformPostion);
            Gizmos.DrawLine(a2+s2, platformPostion);


            Gizmos.color = Color.green;
            Gizmos.DrawSphere(a1, .2f);
            Gizmos.DrawSphere(a2, .2f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(a1+s1, .2f);
            Gizmos.DrawSphere(a2+s2, .2f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(platformPostion, .2f);
        }
    }
}

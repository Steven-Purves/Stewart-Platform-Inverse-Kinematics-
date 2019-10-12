using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixAxis : MonoBehaviour
{
    public Vector3 platformPostion;
    public Vector3 rotationInDegrees;

    float[] baseAngles = { 280, 20, 40, 140, 160, 260 };
    float[] platformAngles = { 315, 345, 75, 105, 195, 225 };

    float sizebase = 2;

    protected Vector3[] baseJoints;
    Vector3[] setPlatformJoints;

    protected Vector3[] legs;

    bool draw;

    private void Awake()
    {
        baseJoints = GetPlateJoints(baseAngles);
    }

    public virtual void Update()
    {
        setPlatformJoints = GetPlateJoints(platformAngles);
        legs = GetLegsPosition();

        draw = true;
       
    }
    Vector3[] GetLegsPosition()
    {
        Vector3[] _legs = new Vector3[setPlatformJoints.Length];
        for (int i = 0; i < setPlatformJoints.Length; i++)
        {
            _legs[i] = platformPostion + RotationMatrix(rotationInDegrees, setPlatformJoints[i]);
        }
        return _legs;
    }
    Vector3 RotationMatrix(Vector3 _rotation,Vector3 _platFormJoint)
    {
        float pitch = _rotation.y * Mathf.Deg2Rad;
        float yaw = _rotation.z * Mathf.Deg2Rad; 
        float roll = _rotation.x * Mathf.Deg2Rad;

        Vector3 firstRow = new Vector3 (Mathf.Cos(yaw) * Mathf.Cos(pitch), (-Mathf.Sin(yaw) * Mathf.Cos(roll)) + (Mathf.Cos(yaw)
         * Mathf.Sin(pitch) * Mathf.Sin(roll)), (Mathf.Sin(yaw) * Mathf.Sin(roll)) + (Mathf.Cos(yaw) * Mathf.Sin(pitch) * Mathf.Cos(roll)));
        Vector3 secondRow = new Vector3 (Mathf.Sin(yaw) * Mathf.Cos(pitch), (Mathf.Cos(yaw) * Mathf.Cos(roll)) + (Mathf.Sin(yaw) * Mathf.Sin(pitch)
         * Mathf.Sin(roll)), (-Mathf.Cos(yaw) * Mathf.Sin(roll)) + (Mathf.Sin(yaw) * Mathf.Sin(pitch) * Mathf.Cos(roll)));
        Vector3 thirdRow = new Vector3(-Mathf.Sin(pitch), Mathf.Cos(pitch) * Mathf.Sin(roll), Mathf.Cos(pitch) * Mathf.Cos(roll));

        Vector3 firstMult = Vector3.Scale(firstRow, _platFormJoint);
        Vector3 secondMult = Vector3.Scale(secondRow, _platFormJoint);
        Vector3 thirdMult = Vector3.Scale(thirdRow, _platFormJoint);

        float x = firstMult.x + firstMult.y + firstMult.z;
        float y = secondMult.x + secondMult.y + secondMult.z;
        float z = thirdMult.x + thirdMult.y + thirdMult.z;

        return new Vector3(x, y, z); 
    }
    Vector3[] GetPlateJoints(float[] _PlateAngles)
    {
        Vector3[] _base = new Vector3[_PlateAngles.Length];

        for (int i = 0; i < baseAngles.Length; i++)
        {
            Vector3 direction = new Vector3(Mathf.Cos(_PlateAngles[i] * Mathf.Deg2Rad), 0, Mathf.Sin(_PlateAngles[i] * Mathf.Deg2Rad));

            _base[i] = direction * sizebase;
        }
        return _base;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (draw)
        {
            Gizmos.color = Color.cyan;
            DrawPlate(baseJoints);
            Gizmos.color = Color.magenta;
            DrawPlate(legs);
            Gizmos.color = Color.yellow;
            DrawLegs(baseJoints, legs);
        }
    }
    void DrawPlate(Vector3[] _plate)
    {
        Vector3 startPos = _plate[0];
        Vector3 prevPos = startPos; 

        foreach (Vector3 _p in _plate)
        {
            Gizmos.DrawSphere(_p ,.05f);
            Gizmos.DrawLine(prevPos, _p);
            prevPos = _p;
        }
        Gizmos.DrawLine(prevPos, startPos);
    }
    void DrawLegs(Vector3[] _baseJoints, Vector3[] _platformJoints)
    {
        for (int i = 0; i < baseJoints.Length; i++)
        {
            Gizmos.DrawLine(_baseJoints[i], _platformJoints[i]);
        }
    }
}

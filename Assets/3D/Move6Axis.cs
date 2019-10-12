using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move6Axis : MonoBehaviour
{
    [SerializeField]
    float speedOfLerp;

    [SerializeField]
    float speedMovement;

    public Vector3 newPosition;
    public Vector3 newRotation;

    float raduis = 1.5f;
    float rotationAmount = 30;

    SixAxis sixAxis;
     

    void Start()
    {
        sixAxis = GetComponent<SixAxis>();
        StartCoroutine(Position()); 
        StartCoroutine(Rotation());
    }

   

    void Update()
    {
        sixAxis.platformPostion = Vector3.Lerp(sixAxis.platformPostion, newPosition, speedOfLerp);
        sixAxis.rotationInDegrees = Vector3.Lerp(sixAxis.rotationInDegrees, newRotation, speedOfLerp *1.5f);
    }
    IEnumerator Position()
    {
        float counter = 0;
        yield return new WaitForSeconds(2);

        while (sixAxis.platformPostion.y < 2)
        {
            newPosition.y = newPosition.y + 0.01f;
            yield return null;
        }

        while (counter < 19)
        {
            counter += Time.deltaTime * speedMovement;
            newPosition = new Vector3(Mathf.Cos(counter) * raduis, sixAxis.platformPostion.y, Mathf.Sin(counter) * raduis);
            yield return new WaitForEndOfFrame();
        }
        newPosition = new Vector3(0, sixAxis.platformPostion.y, 0);
        yield return new WaitForSeconds(8);
        //newPosition = new Vector3(0, .3f, 0);
    }
    IEnumerator Rotation()
    {
        yield return new WaitForSeconds(12);

        float counter = 0;
        while (counter < 25)
        {
            counter += Time.deltaTime * speedMovement;
            newRotation = new Vector3(-Mathf.Sin(counter+90) * rotationAmount, -Mathf.Sin(counter) * rotationAmount, -Mathf.Sin(counter+180) * rotationAmount);
            yield return new WaitForEndOfFrame();
        }
        newRotation = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1);
        newPosition = new Vector3(0, .3f, 0);
    }
}

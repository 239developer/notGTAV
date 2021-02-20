using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject car;
    public float cameraRotSpeed = 180f, cameraSpeed = 0.01f;
    public Vector3 posDelta = new Vector3(0f, 2f, -5f);
    private float k = 1f;

    void Update()
    {
        var pos = transform.position;
        var carPos = car.transform.position;
        var posD = car.transform.TransformVector(posDelta);
        transform.position = Vector3.MoveTowards(pos, carPos + posD * k, (pos - (carPos + posD)).magnitude * cameraSpeed * Time.deltaTime);

        var rot = transform.rotation;
        var carRot = car.transform.rotation;
        transform.rotation = Quaternion.RotateTowards(rot, carRot, cameraRotSpeed * Time.deltaTime * (Quaternion.Angle(rot, carRot)));
    }
}

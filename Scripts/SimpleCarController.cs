using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
     
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public Slider respawn;
    public Vector3 COM = new Vector3(0f, 0f, 1f);
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;
    public float respawnTime;
    private float respawnTimer;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;

    void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = COM;
        startPosition = transform.position;
        startRotation = transform.rotation;
        respawn.maxValue = respawnTime;
    }
     
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    void ApplyForce()
    {
        float brake = maxBrakeTorque * Input.GetAxis("Brake");
        float motor = maxMotorTorque * Input.GetAxis("Motor");
        float steering = maxSteeringAngle * Input.GetAxis("Steer");
     
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
                axleInfo.leftWheel.brakeTorque = brake;
                axleInfo.rightWheel.brakeTorque = brake;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        if(steering == 0f)
            rb.angularVelocity /= 2;
    }

    void Update()
    {
        if(!Input.GetKey(KeyCode.R))
            respawnTimer = Time.time;
        if(Time.time - respawnTimer > respawnTime)
            Respawn();

        respawn.value = Time.time - respawnTimer;
    }
     
    public void FixedUpdate()
    {
        ApplyForce();
    }
}
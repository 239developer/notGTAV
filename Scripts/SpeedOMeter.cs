using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOMeter : MonoBehaviour
{
    public Text text;
    public GameObject car;
    private Rigidbody rb;

    void Start()
    {
        rb = car.GetComponent<Rigidbody>();
    }

    void Update()
    {
        text.text = ((int)rb.velocity.magnitude + 0).ToString() + " m/s";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class softMaker : MonoBehaviour
{
    public SphereCollider prefab;
    private SphereCollider[] col;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vert = mesh.vertices;
        col = new SphereCollider[vert.Length];

        for(int i = 0; i < vert.Length; i++)
        {
            col[i] = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            col[i].center = vert[i];
            col[i].radius = prefab.radius;
        }
    }

    void FixedUpdate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vert = mesh.vertices;

        for(int i = 0; i < vert.Length; i++)
        {
            vert[i] = col[i].center;
        }

        mesh.vertices = vert;
    }   
}

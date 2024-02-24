using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class Attractee : MonoBehaviour
{
    public Attractor attractor;
    private Rigidbody attractee;
    void Awake()
    {
        attractee = GetComponent<Rigidbody>();
        attractee.useGravity = false;
        attractee.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void FixedUpdate()
    {
        attractor.Attract(transform);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attractor : MonoBehaviour
{
    public float gravity = -9.8f;
    public void Attract(Transform attractee)
    {
        Vector3 gravUp = (attractee.position - transform.position).normalized;
        Vector3 attracteeUp = attractee.up;
        Rigidbody rb = attractee.GetComponent<Rigidbody>();
        rb.AddForce(gravUp * gravity); // adds the gravity force
        Quaternion targetRotation = Quaternion.FromToRotation(attracteeUp, gravUp) * attractee.rotation; // keeps object oriented about attractor
        attractee.rotation = Quaternion.Slerp(attractee.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
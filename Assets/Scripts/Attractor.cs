using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float gravity = -9.8f;
    
    public void Attract(Rigidbody attractee) {
        Vector3 gravityUp = (attractee.position - transform.position).normalized;
		Vector3 localUp = attractee.transform.up;

        attractee.AddForce(gravityUp * gravity); // adds the gravity force
		attractee.rotation = Quaternion.FromToRotation(localUp,gravityUp) * attractee.rotation; // keeps object oriented about attractor
    }
}

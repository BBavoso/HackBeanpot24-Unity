using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Attractee : MonoBehaviour {
	
	public Attractor attractor;
	public Rigidbody attractee;
	
	void Awake () {
		attractee.useGravity = false;
		attractee.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () {
		attractor.Attract(attractee);
	}
}
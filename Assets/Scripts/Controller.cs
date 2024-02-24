using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEditor.Callbacks;
[RequireComponent(typeof(Attractee))]
public class Controller : MonoBehaviour
{
    public float moveSpeed = 15;
    private Vector3 moveDir;
    private Rigidbody rb;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}
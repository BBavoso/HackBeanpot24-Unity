using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(Attractee))]
public class Controller : MonoBehaviour
{
    public float moveSpeed = 15;
    private Vector3 moveDir;
    private Rigidbody rb;
    public GameControl  gameControl;
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
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * transform.TransformDirection(moveDir));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trash"))
        {
            Destroy(other.gameObject);
        }
    }

}
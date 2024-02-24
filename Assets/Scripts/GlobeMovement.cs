using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobeMovement : MonoBehaviour
{

    [SerializeField] private float globeMoveSpeed;

    void Update()
    {
        HandleGlobeMovement();
    }

    private void HandleGlobeMovement()
    {
        Vector2 globeMovement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            globeMovement.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            globeMovement.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            globeMovement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            globeMovement.x += 1;
        }

        float rotationModifier = globeMoveSpeed * Time.deltaTime;

        transform.Rotate(globeMovement.y * rotationModifier, -globeMovement.x * rotationModifier, 0, Space.World);


    }
}

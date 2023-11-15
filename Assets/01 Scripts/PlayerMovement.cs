using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    
    Animator animator;
    Vector3 movement;

    private CharacterController characterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();

        // Rotate towards mouse position
        RotateTowardsMouse();

        // Move the player
        characterController.Move(movement * moveSpeed * Time.deltaTime);

        // Set animator parameters for movement
        
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    

        // Set animator parameter for attack
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }

        // Set animator parameter for idle
        if (movement.magnitude == 0)
        {
            animator.SetBool("IsIdle", true);
        }
        else
        {
            animator.SetBool("IsIdle", false);
        }

    }

    void RotateTowardsMouse()
    {
        // Raycast from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            float angle = Mathf.Atan2(targetPoint.x - transform.position.x, targetPoint.z - transform.position.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}

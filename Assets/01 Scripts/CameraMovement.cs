using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;  // Reference to the target 
    public float smoothSpeed = 5f;  // Speed of camera movement
    public float defaultDistance = 5f;  // Default distance from the character
    public float attackingDistance = 2f;  // Distance when character is attacking

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position - (Quaternion.Euler(45, 0, 0) * Vector3.forward) * defaultDistance;

            Animator animator = target.GetComponent<Animator>();
            if (animator != null)
            {
                bool isAttacking = animator.GetBool("IsAttacking");
                float distance = isAttacking ? attackingDistance : defaultDistance;
                desiredPosition = target.position - (Quaternion.Euler(45, 0, 0) * Vector3.forward) * distance;
            }

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.LookAt(target.position);

            Vector3 lookDir = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
            if (lookDir != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                target.rotation = Quaternion.Slerp(target.rotation, rotation, smoothSpeed * Time.deltaTime);
            }
        }
    }
}

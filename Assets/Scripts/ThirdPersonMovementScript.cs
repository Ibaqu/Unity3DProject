using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{

    public CharacterController controller;
    
    // Movement speed of the character
    public float movementSpeed = 5f;

    // Reference to the currently used camera
    public Transform camera;

    // SmoothTime and SmoothVelocity when character turns
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        // Inputs from Keyboard
        float horizontalInput = Input.GetAxis("Horizontal"); //Left Right
        float verticalInput = Input.GetAxis("Vertical"); //Up down

        // Create a basic direction vector with the input values
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f) {
            
            // Calculate the target angle based on the new direction we want to turn
            // When the camera turns, this angle is also considered as the player has to turn to where the camera is looking
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            
            // Add some smoothing to the turn
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            // Rotate the character by that angle
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }
}

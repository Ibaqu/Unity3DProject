using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Movement speed of the character
    public float movementSpeed = 5f;

    // Reference to Master's RigidBody
    public Rigidbody rigidBody;

    public Transform camera;

    // True if on ground, false if in air
    public bool isGrounded = true;

    // SmoothTime and SmoothVelocity when character turns
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal"); //Left Right
        float verticalInput = Input.GetAxis("Vertical"); //Up down

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f) {

            // Atan2 returns angle b/w X-axis and 2D Vector starting at 0 (degrees) and terminating at x,y
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.position = transform.position + moveDirection.normalized * movementSpeed * Time.deltaTime;
        }

        // When space is pressed, add force to the Master's Rigidbody
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        } 
    }

    private void OnCollisionEnter(Collision collision) {
        isGrounded = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimentionalAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f; // Player facing axis
    float velocityX = 0.0f; // Left - Right axis
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;

    int VelocityZHash;
    int VelocityXHash;


    // Start is called before the first frame update
    void Start()
    {
        // Get the `Animator` component of the GameObject this script is attached to
        animator = GetComponent<Animator>();

        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from player
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Set currentMaxVelocity
        // currentMaxVelocity can change depending on whether the run key is pressed
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        // Handle velocity values
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        // Update animator values
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }

    // Handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {

        // Update VelocityZ and VelocityX values when keys are pressed
        // Limit player velocity to currentMaxVelocity
        if (forwardPressed && velocityZ < currentMaxVelocity) {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity) {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPressed && velocityX < currentMaxVelocity) {
            velocityX += Time.deltaTime * acceleration;
        }

        // Update velocityZ value when forward is not pressed
        if (!forwardPressed && velocityZ > 0.0f) {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // Increase velocityX to reach 0.0f when left is not pressed
        if (!leftPressed && velocityX < 0.0f) {
            velocityX += Time.deltaTime * deceleration;
        }

        // Decrease velocityX to reach 0.0f when right is not pressed
        if (!rightPressed && velocityX > 0.0f) {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

     void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity) {
        
        // Reset velocityZ value if it decreases beyond 0.0f
        if (!forwardPressed && velocityZ < 0.0f) {
            velocityZ = 0.0f;
        }

        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)) {
            velocityX = 0.0f;
        }

        //
        // Locking Velocity Values
        //

        // Locking Forward

        // Lock forward velocity when run is pressed
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity) {
            velocityZ = currentMaxVelocity;
        } 
        // If player velocity is greater than currentMaxVelocity, it must be decreased
        else if (forwardPressed && velocityZ > currentMaxVelocity) 
        {
            velocityZ -= Time.deltaTime * deceleration;

            // Round to if it is within the offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f)) {
                velocityZ = currentMaxVelocity;
            }

        }
        // Round to if it is within the offset
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)) {
            velocityZ = currentMaxVelocity;
        }


        // Locking Left

        // Lock left velocity when run is pressed
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity) {
            velocityX = -currentMaxVelocity;
        } 
        // Decelerate to max walk velocity
        else if (leftPressed && velocityX < -currentMaxVelocity) 
        {
            velocityX += Time.deltaTime * deceleration;

            // Round to if it is within the offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f)) {
                velocityX = -currentMaxVelocity;
            }

        }
        // Round to if it is within the offset
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f)) {
            velocityX = -currentMaxVelocity;
        }


        // Locking Right

        // Lock right velocity when run is pressed
        if (rightPressed && runPressed && velocityX > currentMaxVelocity) {
            velocityX = currentMaxVelocity;
        } 
        // Decelerate to max walk velocity
        else if (rightPressed && velocityX > currentMaxVelocity) 
        {
            velocityX -= Time.deltaTime * deceleration;

            // Round to if it is within the offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05)) {
                velocityX = currentMaxVelocity;
            }

        }
        // Round to if it is within the offset
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f)) {
            velocityX = currentMaxVelocity;
        }

    }
}

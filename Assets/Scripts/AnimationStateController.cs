﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    int velocityHash;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");

        if (forwardPressed && velocity < 1.0f) {
            velocity += Time.deltaTime * acceleration; // Increase Player velocity
        }

        if (!forwardPressed && velocity > 0.0f) {
            velocity -= Time.deltaTime * deceleration; // Decrease Player velocity
        }

        if (!forwardPressed && velocity < 0.0f) {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour {

    public GameObject gameObject_PlayerA;
    public GameObject gameObject_PlayerB;
    public ParticleSystem particle_burst;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject_PlayerA.SetActive(true);
        gameObject_PlayerB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button is clicked
        if (Input.GetButtonDown("Fire1")) {

            // Activate the particle system
            Boom();
            
            if (gameObject_PlayerA.activeSelf) {
                gameObject_PlayerA.SetActive(false);
                gameObject_PlayerB.SetActive(true);
            } else {
                gameObject_PlayerA.SetActive(true);
                gameObject_PlayerB.SetActive(false);
            }
        }  
    }

    void Boom() 
    {
        particle_burst.Play();
    }

}

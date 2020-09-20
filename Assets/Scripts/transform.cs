using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour {

    public GameObject gameObject_SphereYellow;
    public GameObject gameObject_SphereGreen;
    public ParticleSystem particle_burst;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject_SphereYellow.SetActive(true);
        gameObject_SphereGreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse button is clicked
        if (Input.GetButtonDown("Fire1")) {

            // Activate the particle system
            Boom();
            
            if (gameObject_SphereYellow.activeSelf) {
                gameObject_SphereYellow.SetActive(false);
                gameObject_SphereGreen.SetActive(true);
            } else {
                gameObject_SphereYellow.SetActive(true);
                gameObject_SphereGreen.SetActive(false);
            }
        }  
    }

    void Boom() 
    {
        particle_burst.Play();
    }

}

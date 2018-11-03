using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrustAudio;

    [SerializeField] float rthrustVal = 250;
    [SerializeField] float uthrustVal = 50;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        thrustAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
    }


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                    print("Friendly Object");
                    break;
        }
    }
    //Vertical thrust movement with sound component
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * uthrustVal);
            if (!thrustAudio.isPlaying)
            {
                thrustAudio.Play();
            }
        }
        else if (thrustAudio.isPlaying)
        {
            thrustAudio.Stop();
        }
    }

    //Rotation of the Space Ship
    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rthrustVal * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate((Vector3.forward) * rotationThisFrame);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-(Vector3.forward) * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

}

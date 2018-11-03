using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;

	[SerializeField] float rthrustVal = 250;
	[SerializeField] float uthrustVal = 50;
    [SerializeField] float levelLoadDelay = 4f;
    [SerializeField] AudioClip thrustSound;
	[SerializeField] AudioClip deathSound;
	[SerializeField] AudioClip winSound;

	[SerializeField] ParticleSystem thrustEffects;
	[SerializeField] ParticleSystem deathEffects;
	[SerializeField] ParticleSystem winEffects;

	enum State {Alive, Dead, Transcending };

	State state;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		state = State.Alive;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}




	private void OnCollisionEnter(Collision collision)
	{
		if (state != State.Alive)
		{
			return;
		}

		switch (collision.gameObject.tag)
		{
			//Friendly not being used anymore
			case "Friendly":
					break;
			case "Finish":
                state = State.Transcending;
				audioSource.PlayOneShot(winSound);
                winEffects.Play();
				Invoke("LoadNextScene",levelLoadDelay);
				break;
			default:
				state = State.Dead;
                if(!deathEffects.isPlaying)
				deathEffects.Play();
				audioSource.PlayOneShot(deathSound);
				Invoke("LoadFirstScene", levelLoadDelay);
				break;
		}
	}

	//TODO: add additional level capability
	private void LoadNextScene()
	{

		SceneManager.LoadScene(1);
	}


	private void LoadFirstScene()
	{
		SceneManager.LoadScene(0);
	}

   
	//Vertical thrust movement with sound component
	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();

		}
		else if (audioSource.isPlaying)
		{
			audioSource.Stop();
			thrustEffects.Stop();
		}
	}

	private void ApplyThrust()
	{
		rigidBody.AddRelativeForce(Vector3.up * uthrustVal);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(thrustSound);
		}
        if (!thrustEffects.isPlaying)
        {
            thrustEffects.Play();
        }
	}


	//Rotation of the Space Ship
	private void RespondToRotateInput()
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

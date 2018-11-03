using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    //Length of units to be moved in world space
    [SerializeField] Vector3 movementVec = new Vector3(10,10,10);
    [SerializeField] float period = 2f;

    const float Pi = 3.14f;

    //A movement Factor to control the swing in Range 0 to 1
    float movementFactor;

    Vector3 startingPosition;

	// Use this for initialization
	void Start ()
    {
        startingPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        float fullSineWave = Mathf.Sin(cycles * Pi * 2);
        movementFactor = (fullSineWave / 2) + 0.5f;
        Vector3 offset = movementVec * movementFactor;
        transform.position = startingPosition + offset;
		
	}
}

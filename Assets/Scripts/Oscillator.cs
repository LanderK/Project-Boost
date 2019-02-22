using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;
    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}

    // Update is called once per frame
    void Update() {
        //protect against divide by 0
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;

        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave / 2f) + 0.5f; 
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}

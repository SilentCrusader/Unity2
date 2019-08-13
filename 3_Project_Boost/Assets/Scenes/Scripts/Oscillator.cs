using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //attribute
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 0f, 0f);
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Protects against NaN
        if(period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period; //grows continually from zero as the game progresses        
        const float tau = Mathf.PI * 2; //6.28 gets you completely around a circle  = 1 cycle
        
        //Creates oscillations of the object that is moving
        float rawSinWave = Mathf.Sin(cycles * tau); //keeps the oscillation between -1 and 1
        float movementFactor = rawSinWave / 2f + 0.5f; //changes the oscillation between 0 and 1

        Vector3 offSet = movementVector * movementFactor;
        transform.position = startingPosition + offSet;
    }
}

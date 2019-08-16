using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 30f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 30f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffSet = xThrow * xSpeed * Time.deltaTime;
        float xNewPosition = Mathf.Clamp(transform.localPosition.x + xOffSet, -xRange, xRange);

        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffSet = yThrow * ySpeed * Time.deltaTime;
        float yNewPosition = Mathf.Clamp(transform.localPosition.y + yOffSet, -yRange, yRange);

        transform.localPosition = new Vector3(xNewPosition, yNewPosition, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        //transform.localRotation = Vector3 //why does sometimes you need a new and other times you don't?!?!
    }
}

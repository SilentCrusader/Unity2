using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 30f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 10f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-Position Based")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float yawFactor = 1.25f;

    [Header("Control-Throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;

    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled == true)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire1"))
        {
            controlGunFire(true);            
        }
        else
        {
            controlGunFire(false);
        }
    }

    private void controlGunFire(bool enableGuns)
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(enableGuns);
        }
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffSet = xThrow * controlSpeed * Time.deltaTime;
        float xNewPosition = Mathf.Clamp(transform.localPosition.x + xOffSet, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffSet = yThrow * controlSpeed * Time.deltaTime;
        float yNewPosition = Mathf.Clamp(transform.localPosition.y + yOffSet, -yRange, yRange);

        transform.localPosition = new Vector3(xNewPosition, yNewPosition, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * pitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }


}

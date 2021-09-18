using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip ("In m/s")][SerializeField] private float controlSpeed = 4f;
    [Tooltip ("In m")][SerializeField] private float xRange = 4f;
    [Tooltip ("In m")][SerializeField] private float yRange = 2.5f;

    [Header("Screen-Position Based")]
    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float positionYawFactor = -5f;
    
    [Header("Control-Throw Based")]
    [SerializeField] private float controlPitchFactor = -5f;
    [SerializeField] private float controlRollFactor = -5f;

    private bool isControlEnabled = true;
    private float xThrow, yThrow;

    private void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
        Debug.Log("Controls frozen");
    }

    private void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessTranslation()
    {
        float clampedXPos = HorizontalMovement();
        float clampedYPos = VerticalMovement();
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        //Due to position only
        float yaw = transform.localPosition.x * positionYawFactor; 
        
        //Due to control throw only
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    private float HorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float clampedXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        return clampedXPos;
    }
    private float VerticalMovement()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float clampedYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);
        return clampedYPos;
    }
}

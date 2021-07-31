using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip ("In m/s")][SerializeField] private float speed = 4f;
    [Tooltip ("In m")][SerializeField] private float xRange = 4f;
    [Tooltip ("In m")][SerializeField] private float yRange = 2.5f;

    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = -5f;
    [SerializeField] private float positionYawFactor = -5f;
    [SerializeField] private float controlRollFactor = -5f;
    

    private float xThrow, yThrow;
    
    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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
        float xOffset = xThrow * speed * Time.deltaTime;
        float clampedXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        return clampedXPos;
    }
    private float VerticalMovement()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * speed * Time.deltaTime;
        float clampedYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);
        return clampedYPos;
    }
}

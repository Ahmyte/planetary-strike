using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //todo work-out why sometimes slow on first play of scene

    [SerializeField] private GameObject[] guns;
    
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

    public void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
    }

    private void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
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

    private void ProcessFiring()
    {
        SetGunsActive(CrossPlatformInputManager.GetButton("Fire"));;
    }

    private void SetGunsActive(bool state)
    {
        foreach (GameObject gun in guns)
        {
            var em = gun.GetComponent<ParticleSystem>().emission;
            em.enabled = state;
        }
    }
}

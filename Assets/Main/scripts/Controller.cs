using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    // Public variables
    public float movementSensitivity = 1f;
    public float walkspeed = 2.5f;
    public float footstepInterval = 0.5f; // Time interval between footstep sounds
    public AudioClip footstepSound; // Footstep sound clip
    public bl_Joystick movement_joystick; // Only keep the movement joystick
    public Camera cam;
    public AudioSource _audiosource;
    public GameObject touchFieldGameObject; // Reference to the GameObject with TouchField.cs
           [Range(0.01f, 1.0f)] public float cameraSensitivity = 0.7f; // Add sensitivity slider in the Inspector

    // Private variables
    private float rotationX = 0f;
    private float rotationY = 0f;
    private CharacterController characterController;
    private float footstepTimer = 0f; // Timer to track footstep sounds
    private TouchField touchField; // Reference to TouchField.cs script
    
    [SerializeField]
    private float footstepIntervalSlider = 0.5f; // Footstep interval slider in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _audiosource = GetComponent<AudioSource>();

        // Find and get the TouchField.cs script attached to the specified GameObject
        touchField = touchFieldGameObject.GetComponent<TouchField>();

        // Set initial camera rotation angles
        rotationX = cam.transform.localRotation.eulerAngles.y;
        rotationY = cam.transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        FPSCamera();
        PlayerControls();

        // Quit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnFootstepIntervalChanged(float value)
    {
        footstepIntervalSlider = value;
    }

    void PlayerControls()
{
    float hInput = Input.GetAxis("Horizontal");
    float vInput = Input.GetAxis("Vertical");

    // Check if joystick input is significant
    bool isJoystickInput = Mathf.Abs(movement_joystick.Horizontal) > 0.3f || Mathf.Abs(movement_joystick.Vertical) > 0.3f;

    // Use joystick input if significant, otherwise use keyboard input
    if (isJoystickInput || (hInput != 0f || vInput != 0f))
    {
        // Use joystick input if significant
        if (isJoystickInput)
        {
            hInput = movement_joystick.Horizontal * movementSensitivity;
            vInput = movement_joystick.Vertical * movementSensitivity;
        }

        Vector3 fwdMovement = Vector3.zero;
        Vector3 rightMovement = Vector3.zero;
        _audiosource.pitch = 0.7f;

        float speed = walkspeed;
        if (characterController.isGrounded)
        {
            fwdMovement = cam.gameObject.transform.forward * vInput;
            rightMovement = cam.gameObject.transform.right * hInput;
        }
       
        if (!_audiosource.isPlaying)
        {
            _audiosource.clip = footstepSound;
            _audiosource.Play();
        }

        characterController.SimpleMove(Vector3.ClampMagnitude(fwdMovement + rightMovement, 1f) * speed);
    }
    else
    {
        _audiosource.Stop();
    }

    // Check if the player is moving significantly
    bool isMoving = Mathf.Abs(hInput) > 0.1f || Mathf.Abs(vInput) > 0.1f;

    // Play footstep sounds at regular intervals while the player is moving and grounded
    if (isMoving && characterController.isGrounded)
    {
        footstepTimer += Time.deltaTime;
        if (footstepTimer >= footstepInterval)
        {
            footstepTimer = 0f;
            PlayFootstepSound();
        }
    }
    else
    {
        footstepTimer = 0f;
    }
}

public void PlayFootstepSound()
{
    // Check if there is a footstep sound assigned
    if (footstepSound != null)
    {
        // Play the footstep sound
        _audiosource.clip = footstepSound;
        _audiosource.Play();
    }
}

    void FPSCamera()
    {
        if (touchField != null) // Check if touchField is not null
        {
            float cameraHorizontal = touchField.touchDist.x;
            float cameraVertical = touchField.touchDist.y;

            // Adjust touch input with camera sensitivity
            cameraHorizontal *= cameraSensitivity;
            cameraVertical *= cameraSensitivity;

            // Check if touch input is significant
            bool isTouchInput = Mathf.Abs(cameraHorizontal) > 0.3f || Mathf.Abs(cameraVertical) > 0.3f;

            // Only update camera rotation if touch input is significant
            if (isTouchInput)
            {
                rotationX += cameraHorizontal;
                rotationY += cameraVertical;

                rotationX = AngleCorrection(rotationX, -360, 360);
                rotationY = AngleCorrection(rotationY, -70, 90); // Use AngleCorrection to clamp rotationY

                Quaternion xQuat = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuat = Quaternion.AngleAxis(rotationY, -Vector3.right);

                Quaternion finalQuat = Quaternion.identity * xQuat * yQuat;

                cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, finalQuat, Time.deltaTime * 20f);
            }
            else
            {
                // Reset rotationY to prevent automatic movement
                rotationY = AngleCorrection(rotationY, -70, 90);
            }
        }
    }

    float AngleCorrection(float angle, float min, float max)
    {
        if (angle > 360)
        {
            angle -= 360;
        }
        if (angle < -360)
        {
            angle += 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Canvas pauseMenuCanvas; // Reference to the pause menu canvas
    public Slider sensitivitySlider; // Reference to the sensitivity slider

    public bool isPaused = false;

    private Controller controllerScript; // Reference to the Controller script

    private void Start()
    {
        // Find the Controller script in the scene
        controllerScript = FindObjectOfType<Controller>();
    }
    public void NoPauseFromStart()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 1f; // Resume the gameplay
            AudioListener.pause = false;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Game is paused
            Time.timeScale = 0f; // Pause the gameplay
            pauseMenuCanvas.enabled = true; // Show the pause menu canvas
            AudioListener.pause = true; // Pause all sounds in the scene
            Debug.Log("Resuming Audio");
        }
        else
        {
            // Game is resumed
            Time.timeScale = 1f; // Resume the gameplay
            pauseMenuCanvas.enabled = false; // Hide the pause menu canvas
            AudioListener.pause = false; // Resume all sounds in the scene
            Debug.Log("Resuming Audio");
        }
    }

    public void OnSensitivitySliderValueChanged()
    {
        // Update the camera sensitivity based on the slider value
        controllerScript.cameraSensitivity = sensitivitySlider.value;
    }
}

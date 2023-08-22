using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject splashScreenImage;
    private Animator splashScreenAnimator;
    private bool isSplashScreenActive = true;

    private void Start()
    {
        // Make sure the splash screen image is active initially
        splashScreenImage.SetActive(true);

        // Get the animator component from the splash screen image object
        splashScreenAnimator = splashScreenImage.GetComponent<Animator>();

        // Start the coroutine to handle the splash screen animation
        StartCoroutine(PlaySplashScreenAnimation());

        // Start the coroutine to deactivate and activate the HUD
    }

    private IEnumerator PlaySplashScreenAnimation()
    {
        // Wait for 5 seconds before deactivating the splash screen
        yield return new WaitForSeconds(5f);

        // Deactivate the splash screen
        splashScreenImage.SetActive(false);
        isSplashScreenActive = false;
    }

    private void Update()
    {
        // Check if the splash screen is still active and blinking
    }
}

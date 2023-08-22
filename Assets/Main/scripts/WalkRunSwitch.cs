using UnityEngine;

public class WalkRunSwitch : MonoBehaviour
{
    public Controller controller;
    private bool isRunning = false;

    private void Start()
    {
        // By default, the player starts walking, so we set the walkspeed at the beginning
        SetWalking();
    }

    public void ToggleWalkRun()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            SetRunning();
        }
        else
        {
            SetWalking();
        }
    }

    private void SetWalking()
    {
        controller.walkspeed = 5f;
    }

    private void SetRunning()
    {
        controller.walkspeed = 8f;

    }
}

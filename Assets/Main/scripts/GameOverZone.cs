using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverZone : MonoBehaviour
{
    // This method is called when another Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as the player
        if (other.CompareTag("Player"))
        {
           
         SceneManager.LoadScene("GameOver");
    
        }
    }
}


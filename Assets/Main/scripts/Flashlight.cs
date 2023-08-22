using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light f_Light;
    public bool isFlashlightOn = true;

    public AudioSource flash_source;
    public AudioClip flash_clip;

    // Start is called before the first frame update
    void Start()
    {
        f_Light.intensity = 7;
    }

    // Update is called once per frame
    void Update()
    {
        FlashControl();
    }

    void FlashControl()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightOn = !isFlashlightOn;
            //play the sound
            //flash_source.clip = flash_clip;
            //flash_source.Play();
        }

        if (isFlashlightOn)
        {
            f_Light.intensity = 7;
        }
        else
        {
            f_Light.intensity = 0;
        }
    }

    public void on_Off()
    {
        isFlashlightOn = !isFlashlightOn;
        //play the sound
        //flash_source.clip = flash_clip;
        //flash_source.Play();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class backstory : MonoBehaviour
{
    public float splashScreenDuration = 5f; // Durasi splash screen dalam detik

    private void Start()
    {
        Invoke("LoadNextScene", splashScreenDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("MainLevel"); // Ganti "NamaSceneBerikutnya" dengan nama scene berikutnya yang ingin ditampilkan setelah splash screen
    }
}

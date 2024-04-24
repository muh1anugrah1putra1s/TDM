using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorSwitcher : MonoBehaviour
{
    public int doorID;  // Unique ID for each door

    public GameObject closedDoor;
    public GameObject openedDoor;
    public Button Button_OpenDoor;
    public Collider doorCollider;
    public float switchDelay = 5f;

    private bool isSwitching = false;
    private bool isPlayerNearby = false;

    private void Start()
    {
        Button_OpenDoor.onClick.AddListener(OpenButtonClicked);
        Button_OpenDoor.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Button_OpenDoor.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Button_OpenDoor.gameObject.SetActive(false);
        }
    }

    private void OpenButtonClicked()
    {
        if (isPlayerNearby && doorCollider.enabled && !isSwitching)
        {
            StartCoroutine(SwitchDoors());
        }
    }

    private IEnumerator SwitchDoors()
    {
        isSwitching = true;

        // Deactivate the closed door and activate the opened door
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);

        yield return new WaitForSeconds(switchDelay);

        // Deactivate the opened door and activate the closed door
        openedDoor.SetActive(false);
        closedDoor.SetActive(true);

        isSwitching = false;
    }
}

using UnityEngine;
using UnityEngine.Events;

public class DoorSensor : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    [SerializeField] UnityEvent OnPlayerEnter;
    [SerializeField] UnityEvent OnPlayerExit;
    [SerializeField] bool playerCheck;

    bool doorActivated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorObj = FindAnyObjectByType<DoorHandler>().GetComponent<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerCheck = collision.gameObject.CompareTag("Player");
        if (playerCheck) 
        {
            doorActivated = true;
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerCheck = collision.gameObject.CompareTag("Player");
        if (playerCheck) 
        {
            doorActivated = false;
            OnPlayerExit?.Invoke();
        }
    }

    public bool DoorActivated()
    {
        return doorActivated;
    }
}

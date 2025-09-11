using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] enum DoorStates
    { 
        Open,
        Closed,
        Moving
    }

    [SerializeField] float doorSpeed = 3f;

    Vector3 closedPosition = new Vector3(3.25f, 2f, 10f);
    Vector3 openedPosition = new Vector3(3.25f, -8f, 10f);

    float closedY = 2f;
    float openY = -8f;

    DoorStates currentDoorState;

    private void Start()
    {
        ChangeDoorYHard(closedPosition.y);
    }

    public void OnPlayerEnter()
    { 
        //Invoked when player enters sensor zone.
        if(currentDoorState != DoorStates.Open)
            ChangeDoorYSoft(openY);
    }

    public void OnPlayerExit()
    { 
        //Invoked when player leaves sensor zone.
        if(currentDoorState != DoorStates.Closed)
            ChangeDoorYSoft(closedY);
    }

    private void ChangeDoorYHard(float inputY)
    {
        DoorStateUpdate();
        transform.position = new Vector3(transform.position.x, inputY, transform.position.z);
        DoorStateUpdate();
    }

    private void ChangeDoorYSoft(float inputY)
    {
        DoorStateUpdate();
        float desiredDistance = transform.position.y - inputY;
        Vector3 directedVector = new Vector3(0f, (desiredDistance), 0f).normalized;

        while(transform.position.y != inputY)
        { 
            transform.Translate(directedVector * Time.deltaTime * doorSpeed);    
        }
        DoorStateUpdate();
    }

    private void DoorStateUpdate()
    {
        float positionY = transform.position.y;
        if(positionY == closedY)
        { 
            currentDoorState = DoorStates.Closed;
        }
        else if(positionY == openY)
        { 
            currentDoorState = DoorStates.Open;
        }
        else
        { 
            currentDoorState = DoorStates.Moving;
        }
    }
}

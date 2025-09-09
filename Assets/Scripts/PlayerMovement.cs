using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float playerMoveSpeed = 6.0f;
    [SerializeField] float playerJumpSpeed = 10.0f;

    Rigidbody playerRB;

    Vector3 movementVector;

    [SerializeField] Animator playerAnimatorController;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimatorController.SetBool("playerRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRB.AddForce(movementVector * playerMoveSpeed, ForceMode.Acceleration);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {   
        if(ctx.performed)
        { 
            playerRB.AddForce(Vector3.up * playerJumpSpeed, ForceMode.Impulse);
        }
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    { 
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        if (ctx.performed)
        { 
            playerAnimatorController.SetBool("playerRunning", true);
        }
        else if (ctx.canceled)
        { 
            playerAnimatorController.SetBool("playerRunning", false);    
        }
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float playerMoveSpeed = 6.0f;
    [SerializeField] float playerJumpSpeed = 10.0f;

    Rigidbody playerRB;
    Transform playerTransform;

    Vector3 movementVector;
    Vector3 movementForwardsRotVector;
    Vector3 animatorForwards;
    Vector3 playerObjForwards;
    Vector3 desiredVector;

    Quaternion playerObjRot;
    Quaternion animatorForwardsRot;
    Quaternion movementForwardsRot;

    public bool playerMoving;

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
        if(playerMoving)
        {
            movementForwardsRot = Quaternion.LookRotation(movementVector, Vector3.up);
            playerObjRot = Quaternion.LookRotation(transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(playerObjRot, movementForwardsRot, (1.0f * Time.deltaTime));    
        }
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
        Debug.DrawRay(transform.position, movementVector, Color.green, 5000);
        
        desiredVector = Vector3.Slerp(transform.position, movementVector, (1.0f * Time.deltaTime)).normalized;
        Debug.DrawRay(transform.position, desiredVector, Color.cyan, 5000);

        //movementForwardsRot = Quaternion.LookRotation(movementVector, Vector3.up);
        //movementForwardsRotVector = movementForwardsRot.eulerAngles;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, guh, (1.0f * Time.deltaTime));

        if (ctx.performed)
        {
            playerMoving = true;
            playerAnimatorController.SetBool("playerRunning", true);
        }
        else if (ctx.canceled)
        {
            playerMoving = false;
            playerAnimatorController.SetBool("playerRunning", false);    
        }
    }

}

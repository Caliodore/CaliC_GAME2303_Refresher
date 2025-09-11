using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float playerMoveSpeed = 6.0f;
    [SerializeField] float playerJumpSpeed = 10.0f;

    Rigidbody playerRB;

    Vector3 movementVector;

    Quaternion playerObjRot;
    Quaternion movementForwardsRot;

    public bool playerMoving;
    public bool isGrounded;

    LayerMask environment;

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
        if(ctx.performed && isGrounded)
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
            playerMoving = true;
            playerAnimatorController.SetBool("playerRunning", true);
        }
        else if (ctx.canceled)
        {
            playerMoving = false;
            playerAnimatorController.SetBool("playerRunning", false);    
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment") && (collision.gameObject.name == "Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment") && (collision.gameObject.name == "Floor"))
        {
            isGrounded = false;
        }
    }

}

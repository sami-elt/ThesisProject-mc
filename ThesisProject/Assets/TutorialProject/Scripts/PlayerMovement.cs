using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody characterRB;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private ParticleSystem dusticles;

    [SerializeField] private float sprint = 2f;

    private Vector3 movementInput;
    private Vector3 movementVector;

    private bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
    }

    private void OnMovement(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();

        movementInput = new Vector3(inputVector.x, 0, inputVector.y);
    }

    private void OnMovementStop(InputValue input)
    {
        movementInput = Vector3.zero;
        movementVector = Vector3.zero;
    }

    private void Sprint(InputValue input)
    {
        isSprinting = input.isPressed;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovement();    
    }

    private void ApplyMovement()
    {
        if (movementInput != Vector3.zero)
        {
         
            movementVector = (movementInput.x * transform.right) + (movementInput.z * transform.forward);

 
            movementVector.y = 0;

            float currentSpeed = movementSpeed;
            if (isSprinting)
            {
                currentSpeed = movementSpeed * sprint;
            }

            characterRB.velocity = movementVector * Time.fixedDeltaTime * currentSpeed;


            if (!dusticles.isPlaying)
            {
                dusticles.Play();
            }
        }
        else
        {
           
            characterRB.velocity = Vector3.zero;

            if (dusticles.isPlaying)
            {
                dusticles.Stop();
            }
        }
    }
}

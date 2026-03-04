using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody characterRB;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private ParticleSystem dustParticles;

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
        Vector2 inputVec = input.Get<Vector2>();

        movementInput = new Vector3(inputVec.x, 0, inputVec.y);
    }

    private void OnMovementStop(InputValue input)
    {
        movementInput = Vector3.zero;
        movementVector = Vector3.zero;
    }

    private void OnSprint(InputValue input)
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


            if (!dustParticles.isPlaying)
            {
                dustParticles.Play();
            }
        }
        else
        {
           
            characterRB.velocity = Vector3.zero;

            if (dustParticles.isPlaying)
            {
                dustParticles.Stop();
            }
        }
    }
}

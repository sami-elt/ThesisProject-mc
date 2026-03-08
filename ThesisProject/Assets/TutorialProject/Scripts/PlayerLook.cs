using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private int mouseSensitivity = 15;
    [SerializeField] private Transform playerCamera;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private float mouseX;
    private float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLook(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        mouseX = inputVector.x;
        mouseY = inputVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        float scaledMouseX = mouseX * Time.deltaTime * mouseSensitivity;
        float scaledMouseY = mouseY * Time.deltaTime * mouseSensitivity;

        xRotation -= scaledMouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 40f);

        yRotation += scaledMouseX;

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent (typeof(Camera))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    #region Gravity
    private float _gravity = -9.81f;
    public float gravityMultiplyer = 1.0f;
    #endregion

    #region Camera
    private Camera _camera;
    private Transform cameraTransform;

    private float cameraVerticalAngle = 0f;

    private float mouseSensivity = 2f;
    #endregion

    #region Jump
    //private float jumpPower = 0f;
    #endregion

    #region Movement
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private float currentSpeed = 0f;
    private float velocityY = 0f;

    private float walkSpeed = 5f;
    private float runMultiplier = 2f;

    private bool canMove = true;
    private bool isSprinting = false;
    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _camera = Camera.main;
        cameraTransform = _camera.transform;
        cameraTransform.rotation = Quaternion.identity;
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraVerticalAngle += mouseY;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -90f, 90f);
        cameraTransform.transform.localEulerAngles = new Vector3(-cameraVerticalAngle, 0, 0);
    }
    private void ApplyGravity()
    {
        if (IsGrounded() && velocityY < 0f)
        {
            velocityY = -1.0f;
        }
        else
        {
            velocityY += _gravity * gravityMultiplyer * Time.deltaTime;
        }

        moveDirection.y = velocityY;
    }

    /*
     * private void ReadInputs()
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.z = Input.GetAxis("Vertical");
        }
     */

    private void ApplyMovement()
    {
        if (!canMove) return;

        var targetSpeed = isSprinting ? walkSpeed * runMultiplier : walkSpeed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, 10 * Time.deltaTime);

        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.z;

        _characterController.Move(move * currentSpeed * Time.deltaTime);
        _characterController.Move(transform.up * moveDirection.y * Time.deltaTime);
    }

    private bool IsGrounded() => _characterController.isGrounded;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y);
    }
}

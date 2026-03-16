using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    #region General Variables

    [Header("Movement & Look")]
    [SerializeField] GameObject camHolder;
    [SerializeField] float speed = 5f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float MaxForce = 1f;
    [SerializeField] float sensitivy = 0.1f;

    [Header("jump and GroundCheck")]

    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [Header("Player stats bools")]

    [SerializeField] bool isSprinting;
    [SerializeField] bool isCrouching;

    #endregion

    Rigidbody rb;
    Animator anim;
    Vector2 moveInput;
    Vector2 lookInput;

    float lookRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim=GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void LateUpdate()
    {
        CameraLook();
    }


    void CameraLook()
    {
        transform.Rotate(Vector3.up * lookInput.x * sensitivy);

        lookRotation += (-lookInput.y * sensitivy);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);

    }

    void Movement()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
        targetVelocity *= isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, MaxForce);


        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    #region Input Methods
    public void Onmove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }


    void Jump()
    {
        if (isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) Jump();

    }
    public void OnCroach(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
        anim.SetBool("isCrouching", isCrouching);

    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isCrouching) isSprinting = true;
        if (context.canceled && isSprinting) isSprinting = false;
    }
    #endregion
}

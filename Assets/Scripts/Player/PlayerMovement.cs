using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sensitivity = 2.0f;

    private Transform playerCamera;
    private float rotationX = 0;

    private Rigidbody rb;

    //Movement
    float moveHorizontal;
    float moveVertical;
    [SerializeField] float sprintMultiplier;
    Vector3 moveDirection;
    Vector3 moveLimits;
    //Camera
    float mouseX;
    float mouseY;
    //Jump
    [SerializeField] float jumpForce;
    bool isGrounded;
    [SerializeField] Transform groundPoint;
    [SerializeField] LayerMask groundMask;
    //Stamina && Hunger
    [SerializeField] CircularSlider staminaSlider;
    [SerializeField] Slider hungerSlider;
    [SerializeField] float staminaCooldown, staminaTimer;
    void Start()
    {
        GameManager.instance.playerMovement = this;
        rb = GetComponent<Rigidbody>();
        moveLimits = new Vector3(moveSpeed, 10, moveSpeed);
        playerCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {       
        CameraController();
        Jump();
    }
    private void FixedUpdate()
    {
        MovementController();
        RecoverStamina();
    }
    public void MovementController()
    {
        // Movimento do personagem
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;

        if (Input.GetButton("Sprint") && isGrounded && staminaSlider.value > 0)
        {
            rb.AddForce(moveDirection * moveSpeed * sprintMultiplier, ForceMode.Impulse);
            rb.velocity = math.clamp(rb.velocity, -moveLimits * sprintMultiplier, moveLimits * sprintMultiplier);
            //StaminaSlider.value -= Time.fixedDeltaTime;
            UseStamina(Time.fixedDeltaTime);
        }
        else
        {
            rb.AddForce(moveDirection * moveSpeed, ForceMode.Impulse);
            rb.velocity = math.clamp(rb.velocity, -moveLimits, moveLimits);
            
        }
    }
    public void RecoverStamina()
    {
        if(staminaTimer >= staminaCooldown)
        {
            if (staminaSlider.value < staminaSlider.maxValue && hungerSlider.value > 0)
            {
                //StaminaSlider.value += 0.5f * Time.fixedDeltaTime;
                //staminaSlider.value += 0.5f * Time.fixedDeltaTime;
                staminaSlider.RemoveValue(Time.fixedDeltaTime);
                hungerSlider.value -= Time.deltaTime;
            }
        }
        else
        {
            staminaTimer += Time.fixedDeltaTime;
        }
    }
    public void UseStamina(float quantity)
    {
        //StaminaSlider.value -= quantity;
        staminaSlider.AddValue(-quantity);
        staminaTimer = 0;
    }
    public void Jump()
    {
        isGrounded = Physics.CheckSphere(groundPoint.position, 0.1f, groundMask);
        // Se o personagem estiver no chão e pressionar a tecla de salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Aplicar força vertical para o salto
            //characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
            rb.AddForce(Vector3.up*jumpForce, ForceMode.VelocityChange);
        }
    }
    public void CameraController()
    {
        if(!Cursor.visible)
        {
            // Rotação da câmera
            mouseX = Input.GetAxis("Mouse X") * sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90, 90);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        }        
    }
}

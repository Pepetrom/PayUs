using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Transform playerCamera;
    private float rotationX = 0;

    private Rigidbody rb;
    //Movement
    public float moveSpeed = 5.0f;
    float moveHorizontal;
    float moveVertical;
    [SerializeField] float sprintMultiplier;
    Vector3 moveDirection;
    private bool isFootstepPlaying = false;
    private bool isRunningPlaying = false;
    //Camera
    public float sensitivity = 2.0f;
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
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;

        if (moveDirection.magnitude > 0.1f && !isFootstepPlaying && isGrounded)
        {
            AudioManager.Instance.PlaySfx("SingleFootstep");
            isFootstepPlaying = true;
            StartCoroutine(WaitForFootstepEnd());
        }

        if (Input.GetButton("Sprint") && isGrounded && staminaSlider.value > 0)
        {
            rb.MovePosition(transform.position + (moveDirection * moveSpeed * sprintMultiplier * Time.fixedDeltaTime));
            UseStamina(Time.fixedDeltaTime);
            if (moveDirection.magnitude > 0.1f && !isRunningPlaying && isGrounded)
            {
                AudioManager.Instance.PlaySfx("RunningBreath");
                isRunningPlaying = true;
                StartCoroutine(WaitForBreathEnd());
            }
        }
        else
        {
            rb.MovePosition(transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime));     
        }
    }
    private IEnumerator WaitForFootstepEnd()
    {
        yield return new WaitForSeconds(AudioManager.Instance.GetSfxClipLength("SingleFootstep"));
        isFootstepPlaying = false;
    }
    private IEnumerator WaitForBreathEnd()
    {
        yield return new WaitForSeconds(AudioManager.Instance.GetSfxClipLength("RunningBreath"));
        isRunningPlaying = false;
    }
    public void RecoverStamina()
    {
        if(staminaTimer >= staminaCooldown)
        {
            if (staminaSlider.value < staminaSlider.maxValue)
            {
                staminaSlider.RemoveValue(Time.fixedDeltaTime);
            }
        }
        else
        {
            staminaTimer += Time.fixedDeltaTime;
        }
    }
    public void UseStamina(float quantity)
    {
        staminaSlider.AddValue(-quantity);
        staminaTimer = 0;
    }
    public void Jump()
    {
        isGrounded = Physics.CheckSphere(groundPoint.position, 0.1f, groundMask);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {           
            rb.AddForce(Vector3.up*jumpForce, ForceMode.VelocityChange);
        }
    }
    public void CameraController()
    {
        if(!Cursor.visible)
        {
            mouseX = Input.GetAxis("Mouse X") * sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90, 90);

            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Door") == true)
        {
            SceneManager.LoadScene("VillageTemp");
        }
    }
}

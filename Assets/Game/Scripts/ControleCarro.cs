using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControleCarro : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public PlayerLogic player;
    public PlayerMovement playerMove;
    public bool isDriving = false;
    public float maxTorque = 200f;
    public float maxSteerAngle = 30f;
    public float brakeTorque = 1000f;
    float horizontalInput;
    float verticalInput;
    public Transform cameraRotation;
    public Camera playerCam, carCam;
    public GameObject steeringWheel;
    private void Start()
    {
        player = GameManager.instance.playerLogic;
        playerMove = GameManager.instance.playerMovement;
        playerCam = Camera.main;
        carCam.enabled = false;
        cameraRotation = carCam.transform;
    }
    void FixedUpdate()
    {
        if (isDriving)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            ApplySteering(horizontalInput);
            ApplyAcceleration(verticalInput);
            ApplyBraking();
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
        if (carCam.enabled)
        {
            carCam.transform.rotation = cameraRotation.transform.rotation;
        }
    }
    private void ApplySteering(float steer)
    {
        float steerAngle = maxSteerAngle * steer;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;
        steeringWheel.transform.Rotate(Vector3.forward * steer *10);
    }

    private void ApplyAcceleration(float throttle)
    {
        float torque = maxTorque * throttle;   
        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isDriving = player.EnterCar(this.transform);
                playerCam.enabled = !playerCam.enabled;
                carCam.enabled = !carCam.enabled;
                playerMove.canMove = !isDriving;
                player.gameObject.SetActive(!isDriving);
            }
        }
    }
    private void ApplyBraking()
    {
        bool isBraking = Input.GetKey(KeyCode.Space);

        if (isBraking)
        {
            rearLeftWheel.brakeTorque = brakeTorque;
            rearRightWheel.brakeTorque = brakeTorque;
        }
        else
        {
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }
    }
}

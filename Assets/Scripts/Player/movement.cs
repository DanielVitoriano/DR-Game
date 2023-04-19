using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class movement : MonoBehaviour
{
    /* variaveis */
    private Vector3 movementDirection;
    private float characterSpeed;
    private const float runningSpeed = 8f;
    private const float walkingSpeed = 2f;
    private float characterJumpForce = 1000;
    private bool isRunning = false;

    /* components */
    private CinemachineVirtualCamera characterCamera;
    private Animator characterAnimator;
    private CharacterController characterController;

    void Awake(){
        characterCamera = FindObjectOfType<CinemachineVirtualCamera>();
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
        SetAnim();
    }

    private void Move(){
        movementDirection = Vector3.zero;
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementDirection.Normalize();

        if (Input.GetButton("Run") && !isRunning && movementDirection.z > 0.5f)
        {
            isRunning = true;
        }else if(Input.GetButton("Run") && isRunning && movementDirection.z > 0.5f){
            isRunning = false;
        }else if(isRunning && movementDirection.z < 0.5){
            isRunning = false;
        }

        characterSpeed = isRunning ? runningSpeed : walkingSpeed;

        if(movementDirection.x != 0 || movementDirection.z != 0){
            Vector3 cameraForward = characterCamera.transform.forward;
            cameraForward.y = 0f;
            transform.rotation = Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0f, 0f, 0f);
        }

        Vector3 currentMovement = transform.rotation * movementDirection;
        characterController.Move(currentMovement * Time.fixedDeltaTime * characterSpeed);
    }

    private void SetAnim(){
        characterAnimator.SetFloat("X", movementDirection.x);
        characterAnimator.SetFloat("Z", movementDirection.z);

        //characterAnimator.SetBool("isJumping", isJumping);

        characterAnimator.SetBool("isRunning", isRunning);
    }

}

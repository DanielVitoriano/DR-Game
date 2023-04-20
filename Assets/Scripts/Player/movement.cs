using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class movement : MonoBehaviour
{
    /* variaveis */
    private Vector3 movementDirection;
    private float characterSpeed;
    private const float runningSpeed = 6f;
    private const float walkingSpeed = 1.5f;
    private float characterJumpForce = 25;
    private bool isRunning = false;
    private bool isJumping = false;
    private float gravity = 9.81f;

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

    if(movementDirection != Vector3.zero){
        Vector3 cameraForward = characterCamera.transform.forward;
        cameraForward.y = 0f;
        transform.rotation = Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0f, 0f, 0f);
    }

    Vector3 currentMovement = transform.rotation * movementDirection;
    currentMovement.y = 0;

    if(!isJumping && characterController.isGrounded && Input.GetButton("Jump")){
        isJumping = true;
        currentMovement.y += characterJumpForce;
    }
    else if(characterController.isGrounded && isJumping){
        isJumping = false;
    }

    if (!characterController.isGrounded) {
        currentMovement.y -= gravity * Time.fixedDeltaTime;
    }
        
    characterController.Move(currentMovement * Time.fixedDeltaTime * characterSpeed);
}


    private void SetAnim(){
        characterAnimator.SetFloat("X", movementDirection.x);
        characterAnimator.SetFloat("Z", movementDirection.z);

        characterAnimator.SetBool("isJumping", isJumping);
        characterAnimator.SetBool("isRunning", isRunning);
    }

}

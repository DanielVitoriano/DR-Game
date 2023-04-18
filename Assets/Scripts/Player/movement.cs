using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class movement : MonoBehaviour
{

    private CharacterController characterController;
    private Animator anim;
    private Vector3 inputVector;
    private float speed = 2f;
    private float speedBase;
    private bool isRunning = false;
    private CinemachineVirtualCamera  cm;
    private bool isOnFloor = true;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        speedBase = speed;
        cm = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void FixedUpdate() {
        Move();
        SetAnim();
        Running();
        Jump();
    }

    private void Running(){
        float targetSpeed = isRunning ? speedBase * 5.0f : speedBase;
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 3.0f);
        
        isRunning = Input.GetButton("Run");
        anim.SetBool("isRunning", isRunning);

    }

    private bool IsGrounded() {
        float distanceToGround = 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround + 0.1f)) {
            return true;
        } else {
            return false;
        }
    }

    private void Jump(){
        isOnFloor = IsGrounded();
        if(Input.GetAxis("Jump") == 1 && isOnFloor){
            anim.SetBool("isJumping", true);
        }else{
            anim.SetBool("isJumping", false);
        }
    }

    private void Move(){
        inputVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputVector.Normalize();

        Vector3 cameraForward = cm.transform.forward;
        cameraForward.y = 0f;

        if(inputVector != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(cameraForward) * Quaternion.Euler(0f, 0f, 0f);
        }

        if(inputVector.z < 0){
            isRunning = false;
            characterController.Move(inputVector * (speed * 0.5f) * Time.fixedDeltaTime);
            return;
        }

        if(inputVector.x > 0 && inputVector.x < 1){
            isRunning = false;
            characterController.Move(inputVector * (speed * 0.7f) * Time.fixedDeltaTime);
            return;
        }

        Vector3 moveDirection = transform.rotation * inputVector;
        characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
    }

    private void SetAnim(){
        anim.SetFloat("X", inputVector.x);
        anim.SetFloat("Z", inputVector.z);
    }
}

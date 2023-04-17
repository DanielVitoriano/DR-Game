using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    private CharacterController characterController;
    private Animator anim;
    private Vector3 inputVector;
    private float speed = 2f;
    private float speedBase;
    public bool isRunning = false;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        speedBase = speed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate() {
        Move();
        SetAnim();
        Running();
    }

    private void Running(){
        float targetSpeed = isRunning ? speedBase * 5.0f : speedBase;
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * 3.0f);

        isRunning = Input.GetButton("Run");
        anim.SetBool("isRunning", isRunning);

    }

    private void Move(){
        inputVector.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputVector.Normalize();

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

        characterController.Move(inputVector * speed * Time.fixedDeltaTime);
    }

    private void SetAnim(){
        anim.SetFloat("X", inputVector.x);
        anim.SetFloat("Z", inputVector.z);
    }
}

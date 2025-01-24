using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public CharacterController cc;
    public bool acceptingInputs;
    public static FishController instance;
    public float characterTurnSpeed;
    public GameObject model;

    public GameObject camArm;
    public Camera cam;
    //public bool controlling;

    public float moveSpeed;
    private float adjustedSpeed;
    public float mouseSens;

    private Vector3 movementDirection;
    private Vector2 lookXY;

    public float SprintMultiplier;

    private Vector3 lastPos;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    private void Start()
    {
        PlayerCursor(false);
    }

    void Update()
    {
        AssignVariables();
        MovementUpdate();
        CameraUpdate();
        CheckInputs();

    }
    private void LateUpdate()
    {
        lastPos = transform.position;
    }
    private void AssignVariables()
    {
        //Left / Right
        float lookX = Input.GetAxis("Mouse X") * Time.smoothDeltaTime * mouseSens;
        //Up / Down
        float lookY = Input.GetAxis("Mouse Y") * Time.smoothDeltaTime * mouseSens * -1;

        lookXY.x = Mathf.Clamp(lookXY.x + lookY, -90, 90);
        lookXY.y += lookX;

        //Forward / Backward
        float fb = Input.GetAxis("Vertical");

        //Left / Right
        float lr = Input.GetAxis("Horizontal");

        movementDirection = cam.transform.forward * fb + cam.transform.right * lr;
        movementDirection.y = 0;
        movementDirection = movementDirection.normalized;

        adjustedSpeed = moveSpeed;

    }
    public bool disallowSprinting;
    private bool CanSprint()
    {
        bool result = true;
        result &= !disallowSprinting;
        return result;
    }
 
    private void CameraUpdate()
    {
        camArm.transform.rotation = Quaternion.Euler(new Vector3(lookXY.x, lookXY.y, 0));
        
        //model.transform.LookAt(transform.position + movementDirection);
        Vector3 lookPoint = Vector3.RotateTowards(model.transform.forward, movementDirection, characterTurnSpeed, characterTurnSpeed);
        lookPoint = transform.position + lookPoint;
        model.transform.LookAt(lookPoint);
        model.transform.eulerAngles = new Vector3(0, model.transform.eulerAngles.y, 0);
    }
    private void MovementUpdate()
    {
        cc.Move(movementDirection * Time.deltaTime * adjustedSpeed);

    }
    private void CheckInputs()
    {
        
    }

    public void PlayerCursor(bool value)
    {
        acceptingInputs = !value;
        Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = value;
    }


}

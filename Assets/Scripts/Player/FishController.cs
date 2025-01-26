using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishController : MonoBehaviour
{
    public CharacterController cc;
    public bool acceptingInputs;
    public static FishController instance;
    public float characterTurnSpeed;
    public GameObject model;

    public float OxygenAmount = 1f;
    public float OxygenAmountDecrease = 0.01f;
    public Slider slider;

    public bool PauseOxygen = false;

    public Player playerStats;

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

    public Animator animator;

    float blendAnim;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    public void ShowBite()
    {
        animator.SetBool("Biting", true);
        StartCoroutine(DisableBite());

    }

    private IEnumerator DisableBite()
    {
        yield return null;
        animator.SetBool("Biting", false);
    }

    private void Start()
    {
        PlayerCursor(false);
    }

    private void FixedUpdate()
    {
        if (PauseOxygen == false)
        {
            OxygenAmount = OxygenAmount - OxygenAmountDecrease;
            slider.value = OxygenAmount;
            if(OxygenAmount <= 0)ShorkDie();

        }
    }
    public GameObject DeathUI;
    public void ShorkDie()
    {
        //activate UI
        if(DeathUI != null) DeathUI.SetActive(true);
        PlayerCursor(true);
        Time.timeScale = 0.01f;

    }

    public int dayNNites;

    public void UpdateOxygen(float add)
    {
        add = Mathf.Clamp((add/dayNNites), 1, 50);
        print(add);
        instance.OxygenAmount = Mathf.Clamp(instance.OxygenAmount + (instance.slider.maxValue * (add / 100f)), instance.slider.minValue, instance.slider.maxValue);
        slider.value = instance.OxygenAmount;
    }

    void Update()
    {
        if (!acceptingInputs) return;
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

        adjustedSpeed = Mathf.Clamp(adjustedSpeed, 5, 50);

        blendAnim += movementDirection.magnitude > 0 ? Time.deltaTime * 3f : -Time.deltaTime * 3f;
        blendAnim = Mathf.Clamp01(blendAnim);
        animator.SetFloat("Blend", blendAnim);

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
    public float Gravity;
    private void MovementUpdate()
    {
        cc.Move(movementDirection * Time.deltaTime * adjustedSpeed);
        //Gravity
        cc.Move(Vector3.down * Gravity * Time.deltaTime);

    }
    private void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("CombinedScene");
        }
    }

    public void PlayerCursor(bool value)
    {
        acceptingInputs = !value;
        Cursor.lockState = value ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = value;
    }


}

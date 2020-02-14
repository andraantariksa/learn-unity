using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform rootPlayer;
    [SerializeField]
    private Transform rootPlayerLook;
    [SerializeField]
    private bool inverted;
    [SerializeField]
    private bool unlockable;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private int smoothSteps;
    [SerializeField]
    private float smoothWeigth;
    [SerializeField]
    private float rollAngle;
    [SerializeField]
    private float rollSpeed;
    [SerializeField]
    private Vector2 defaultLookLimit;
    private Vector2 lookAngle;
    private Vector2 currentMouseLook;
    private float currentRollAngle;
    private int lastLookFrame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        this.CursorLock();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            this.LookAround();
        }
    }

    void CursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ?
                CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    void LookAround()
    {
        this.currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        this.lookAngle.x += this.currentMouseLook.x * this.sensitivity * (this.inverted ? 1.0f : -1.0f);
        this.lookAngle.y += this.currentMouseLook.y * this.sensitivity;

        this.lookAngle.x = Mathf.Clamp(this.lookAngle.x, this.defaultLookLimit.x, this.defaultLookLimit.y);
        
        this.currentRollAngle = Mathf.Lerp(
            this.currentRollAngle,
            Input.GetAxisRaw("Mouse X") * this.rollAngle,
            Time.deltaTime * this.rollSpeed
        );

        this.rootPlayerLook.localRotation = Quaternion.Euler(this.lookAngle.x, 0.0f, this.currentRollAngle);
        
        this.rootPlayer.localRotation = Quaternion.Euler(0.0f, this.lookAngle.y, 0.0f);
    }
}

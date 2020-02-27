﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerPhysics phys;
    Rigidbody rb;
    Camera camera;
    public bool onGround;

    #region Serialized
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpSpeed = 1.0f;
    [SerializeField]
    float yeetSpeed = 1.0f;
    [SerializeField]
    float sensitivityX = 15f;
    [SerializeField]
    float sensitivityY = 15f;
    [SerializeField]
    float minimumX = -360F;
    [SerializeField]
    float maximumX = 360F;
    [SerializeField]
    float minimumY = -60F;
    [SerializeField]
    float maximumY = 60F;
    #endregion

    float rotX, rotY;
    Quaternion originalRotation;

    private void Awake()
    {
        phys = GetComponentInParent<PlayerPhysics>();
        camera = FindObjectOfType<Camera>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        //MouseRot();
    }


    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html and adapted to suit my game
    void MouseRot()
    {
        rotX += Input.GetAxis("Mouse X") * sensitivityX;
        rotY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotX = ClampAngle(rotX, minimumX, maximumX);
        rotY = ClampAngle(rotY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        //camera.transform.localRotation = Quaternion.AngleAxis(phys.gravObj.myAngle, camera.transform.forward);
    }

    void HandleInput()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * speed, Space.Self);
        transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed, Space.Self);

        LineRenderer lr = new LineRenderer();

        if(Input.GetMouseButtonDown(0) && phys.grav)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                FlyTo(hit.collider.gameObject);
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && onGround)
        {
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void FlyTo(GameObject target)
    {
        if(target == phys.gravObj) { return; } //Do not allow jumping to the same object
        phys.grav = false;
        //phys.gravObj = target.GetComponent<PlatformRotator>();
        rb.velocity = (target.transform.position - transform.position).normalized * yeetSpeed;
    }


    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

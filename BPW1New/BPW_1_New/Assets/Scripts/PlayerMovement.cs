using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float lateralSpeed, verticalSpeed;
    public float jumpHeight;
    public bool onGround = true;
    public Vector3 cameraOffset;
    public float sensitivityX, sensitivityY, minimumX, maximumX, minimumY, maximumY;
    float rotX, rotY;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    #region input and movement
    void HandleInput()
    {
        MouseRot();

    }

    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html and adapted to suit my game
    void MouseRot()
    {
        rotX += Input.GetAxis("Mouse X") * sensitivityX;
        rotY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotX = ClampAngle(rotX, minimumX, maximumX);
        rotY = ClampAngle(rotY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotX, transform.forward);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion*yQuaternion;
        //Camera.main.transform.localRotation = originalRotation * yQuaternion;

        transform.position += VectorOnPlane(transform.right, Vector3.up) * -rotX * lateralSpeed * Time.deltaTime;
        transform.position += Vector3.up * rotY * verticalSpeed * Time.deltaTime;
    }
    #endregion
    #region Collision
    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Floor": onGround = true; break;
            default: break;
        }
    }
    #endregion
    #region helpers
    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html
    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    Vector3 VectorOnPlane(Vector3 vector, Vector3 planeNormal)
    {
        return Vector3.ProjectOnPlane(vector, planeNormal);
    }
    #endregion
}

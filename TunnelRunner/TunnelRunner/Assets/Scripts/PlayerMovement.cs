using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerPhysics phys;
    Rigidbody rb;
    Camera camera;
    public bool onGround;
    public bool canMove = true;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        MouseRot();
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
    }

    void HandleInput()
    {
        if (!canMove) { return; }
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * speed, Space.Self);
        float xsp = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(new Vector3(transform.forward.x * xsp, 0, transform.forward.z * xsp), Space.World);


        if(Input.GetMouseButtonDown(0) && phys.grav)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                FlyTo(hit);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void FlyTo(RaycastHit hit)
    {
        PlatformRotator target = hit.collider.gameObject.GetComponent<PlatformRotator>();

        if(target == null) { return; }
        if(target == phys.gravObj) { return; } //Do not allow jumping to the same object

        canMove = false;
        
        phys.grav = false;
        PlatformRotator oldGO = phys.gravObj;
        //phys.gravObj = target.GetComponent<PlatformRotator>();
        rb.velocity = (/*target.transform.position */ hit.point - transform.position).normalized * yeetSpeed;


        //rb.velocity = (convertedHitPoint - transform.position).normalized * yeetSpeed;
        //transform.position = convertedHitPoint;
    }


    void RotateLevel()
    {

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

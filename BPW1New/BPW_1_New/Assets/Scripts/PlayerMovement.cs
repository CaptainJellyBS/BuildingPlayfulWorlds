using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static GameObject Instance { get; private set; }

    public float lateralSpeed, verticalSpeed;
    public float jumpHeight;
    public bool onGround = true;
    public float bulletHorOffset, bulletVerOffset;
    public float sensitivityX, sensitivityY, minimumX, maximumX, minimumY, maximumY;
    public float xRotationDeadzoneAngle, yRotationDeadzoneAngle;
    public float fireDelay, reloadTime;
    float rotX, rotY;
    Quaternion originalRotation;
    public bool canMove = false;
    public bool canShoot = false;
    public GameObject bullet;
    public AmmoPanel[] ammoPanels;
    public int currentBullet = 0;
    int maxBullets;

    // Start is called before the first frame update
    void Start()
    {
        Instance = gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalRotation = transform.localRotation;
        maxBullets = ammoPanels[0].bulletIcons.Length;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    #region input and movement

    //Mouse rotation code taken from: https://answers.unity.com/questions/29741/mouse-look-script.html and adapted to suit my game
    void HandleInput()
    {
        if (!canMove) { return; }
        //Mouse Rotation
        rotX += Input.GetAxis("Mouse X") * sensitivityX;
        rotY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotX = ClampAngle(rotX, minimumX, maximumX);
        rotY = ClampAngle(rotY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotX, transform.forward);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        //Calculate lateral and vertical movement from Mouse Rotation, with a deadzone
        if (Mathf.Abs(rotX) > xRotationDeadzoneAngle)
        {
            transform.position += VectorOnPlane(transform.right, Vector3.up) * -rotX * lateralSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(rotY) > yRotationDeadzoneAngle)
        {
            transform.position += Vector3.up * rotY * verticalSpeed * Time.deltaTime;
        }

        if(Input.GetMouseButton(0))
        {
            if (canShoot)
            {
                Instantiate(bullet, new Vector3(transform.position.x + bulletHorOffset, transform.position.y + bulletVerOffset, transform.position.z), Quaternion.identity);
                Instantiate(bullet, new Vector3(transform.position.x - bulletHorOffset, transform.position.y + bulletVerOffset, transform.position.z), Quaternion.identity);
                AudioM.Instance.PlayLaserSound();
                StartCoroutine(ReloadBullets());
            }
        }
    }

    #endregion

    #region Collision
    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Floor": onGround = true; break;
            case "Obstacle":
            case "Mine":
            case "Death": Die(); break;
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

    IEnumerator ReloadBullets()
    {
        canShoot = false;

        foreach (AmmoPanel a in ammoPanels) { a.RemoveBullet(currentBullet); }
        currentBullet++;

        if (currentBullet < maxBullets)
        {
            yield return new WaitForSeconds(fireDelay);
        }
        else
        {
            yield return new WaitForSeconds(reloadTime);
            currentBullet = 0;
            foreach (AmmoPanel a in ammoPanels) { a.ReloadBullets(); }
        }

        canShoot = true;

    }
    #endregion

    #region intro Coroutine
    public void StartIntro()
    {
        StartCoroutine(IntroRoutine());
    }

    public IEnumerator IntroRoutine()
    {
        float t = 0.0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, 15, startPos.z);
        
        float risingTime = 6.0f;
        while(t<1.0f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime/risingTime;
            yield return null;
        }
    }

    public IEnumerator TutorialLateral()
    {
        float startX = transform.position.x;
        bool done = false;
        while(!done)
        {
            done = transform.position.x > startX + 2 || transform.position.x < startX - 2;
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
    }

    public IEnumerator TutorialVertical()
    {
        float startY = transform.position.y;
        bool done = false;
        while (!done)
        {
            done = transform.position.y > startY + 2 || transform.position.x < startY - 2;
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
    }
    public IEnumerator TutorialShoot()
    {
        float startY = transform.position.y;
        bool done = false;
        while (!done)
        {
            done = Input.GetMouseButton(0);
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
    }
    #endregion

    #region Death

    void Die()
    {
        GameManager.Instance.Die();
    }

    #endregion
}

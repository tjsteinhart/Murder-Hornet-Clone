using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HornetController : MonoBehaviour
{
    [SerializeField] float currentSpeed = .1f;
    [SerializeField] float speedModifier = .02f;
    [SerializeField] float currentSting = 10;
    [SerializeField] float stingModifier = 2f;
    [SerializeField] float rotationspeed = 100f;
    [SerializeField] bool isMoving;

    public float CurrentSpeed() => currentSpeed;
    public float CurrentSting() => currentSting;

    Animator hornetAnimator;
    Rigidbody rb;

    //Variables for moving with touch
    Touch touch;
    Vector2 touchPos;
    Quaternion rotation;
    float touchRotateSpeedModifier = .1f;

    private void Start()
    {
        isMoving = false;
        hornetAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        UpdateMovement();
        EventManager.Instance.onEndGamePlay += EndMovement;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onEndGamePlay -= EndMovement;
        }
    }

    private void UpdateMovement()
    {
        currentSting = currentSting + GameManager.Instance.GetStingForceModifier() * stingModifier;
        currentSpeed = currentSpeed + GameManager.Instance.GetPlayerSpeedModifier() * speedModifier;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            HornetMovement();
        }
    }

    private void HornetMovement()
    {
        //Forward Movement
        transform.Translate(Vector3.forward * currentSpeed);

        //Rotation Using Mouse
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime, 0, Space.World);
            // rotate around local X
            transform.Rotate(-Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime, 0, 0);
        }

        //Rotation Using Touch
        //if (Input.touchCount > 0)
        //{
        //    touch = Input.GetTouch(0);
        //    if(touch.phase == TouchPhase.Moved)
        //    {
        //        transform.Rotate(0f, touch.deltaPosition.x * touchRotateSpeedModifier, 0f, Space.World);
        //        transform.Rotate(-touch.deltaPosition.y * touchRotateSpeedModifier, 0f, 0f);
        //    }
        //}
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        //var targetController = other.GetComponent<TargetController>();
        IGetStung target = other.GetComponent<IGetStung>();
        TargetController targetController = other.GetComponent<TargetController>();
        if(target != null)
        {
            StartCoroutine(StingAttack(other.gameObject));
            //other.GetComponentInChildren<Rigidbody>().AddForce(this.transform.forward * currentSting, ForceMode.Impulse);
            target.GetStung();
        }
        if(targetController != null)
        {
            targetController.GetMyRigidBody().AddForce(this.transform.forward * currentSting, ForceMode.Impulse);
        }
    }

    IEnumerator StingAttack(GameObject other)
    {
        isMoving = false;
        hornetAnimator.SetTrigger("HornetSting");
        Handheld.Vibrate();
        yield return new WaitForSeconds(.5f);
        isMoving = true;
    }

    private void EndMovement()
    {
        isMoving = false;
        rb.velocity = Vector3.zero;
    }

}

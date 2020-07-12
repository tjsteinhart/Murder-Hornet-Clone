using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HornetController : MonoBehaviour
{
    [SerializeField] float currentSpeed = 3f;
    [SerializeField] float speedModifier = .5f;
    [SerializeField] float currentSting = 1;
    [SerializeField] float stingModifier = 2f;
    [SerializeField] float rotationspeed = 100f;
    [SerializeField] bool isMoving;

    public float CurrentSpeed() => currentSpeed;
    public float CurrentSting() => currentSting;

    Animator hornetAnimator;
    TargetManager targetManager;
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
        targetManager = FindObjectOfType<TargetManager>();
    }

    private void OnEnable()
    {
        UpdateMovement();
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
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        //Rotation Using Mouse
        //if (Input.GetMouseButton(0))
        //{
        //    transform.Rotate(0, Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime, 0, Space.World);
        //    // rotate around local X
        //    transform.Rotate(-Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime, 0, 0);
        //}

        //Rotation Using Touch
        if (Input.touchCount > 0)
        {
            Debug.Log("Received touch: " + Input.touchCount);
            touch = Input.GetTouch(0);
            Debug.Log(touch.phase);

            if (touch.phase == TouchPhase.Moved)
            {
                
                transform.Rotate(0f, touch.deltaPosition.x * touchRotateSpeedModifier, 0f, Space.World);
                transform.Rotate(-touch.deltaPosition.y * touchRotateSpeedModifier, 0f, 0f);
            }
        }
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        IGetStung target = other.GetComponent<IGetStung>();

        TargetController targetController = other.GetComponent<TargetController>();
        if(target != null)
        {
            target.GetStung();
            StartCoroutine(StingAttack(other.gameObject));
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
        if (targetManager.GetAllTargetsHit())
        {
            isMoving = false;
            yield return new WaitForSeconds(2f);
            EventManager.Instance.EndGamePlay();
            yield break;
        }

        yield return new WaitForSeconds(.5f);
        isMoving = true;
    }
}

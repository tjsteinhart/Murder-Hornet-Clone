using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HornetController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = .1f;
    [SerializeField] float rotationspeed = 100f;
    [SerializeField] float baseStingForce = 10;
    [SerializeField] bool isMoving;

    Animator hornetAnimator;
    Rigidbody rb;

    private void Start()
    {
        isMoving = false;
        hornetAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += UpdateMovement;
        EventManager.Instance.onEndGamePlay += EndMovement;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= UpdateMovement;
            EventManager.Instance.onEndGamePlay -= EndMovement;
        }
    }

    private void UpdateMovement()
    {
        baseStingForce *= (GameManager.Instance.GetStingForceModifier());
        forwardSpeed *= GameManager.Instance.GetPlayerSpeedModifier();
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
        transform.Translate(Vector3.forward * forwardSpeed);
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime, 0, Space.World);
            // rotate around local X
            transform.Rotate(-Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime, 0, 0);

        }
        rb.angularVelocity = Vector3.zero;
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Target")
    //    {
    //        StartCoroutine(StingAttack(other.gameObject));
    //        EventManager.Instance.TargetStung(other.GetComponent<TargetController>());
    //        other.enabled = false;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        var targetController = collision.gameObject.GetComponent<TargetController>();
        if(collision.gameObject.tag == "Target" && targetController.CanSting())
        {
            StartCoroutine(StingAttack(collision.gameObject));
            EventManager.Instance.TargetStung(targetController);
            targetController.SetCanSting(false);
        }
        rb.velocity = Vector3.zero;
    }

    IEnumerator StingAttack(GameObject other)
    {
        isMoving = false;
        hornetAnimator.SetTrigger("HornetSting");
        Handheld.Vibrate();
        other.GetComponent<Rigidbody>().AddForce(this.transform.forward * baseStingForce, ForceMode.Impulse);
        other.GetComponent<TargetController>().PlayExclamation();
        yield return new WaitForSeconds(.5f);
        isMoving = true;
    }

    private void EndMovement()
    {
        isMoving = false;
        rb.velocity = Vector3.zero;
    }

}

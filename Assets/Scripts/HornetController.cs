using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HornetController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = .1f;
    [SerializeField] float rotationspeed = 100f;
    [SerializeField] float baseStingForce = 10;
    [SerializeField] bool isMoving = false;

    Vector3 dragOffset;
    Vector3 startDragPos;
    Vector3 rotateDrag;
    private Quaternion rotation;

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += UpdateMovement;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= UpdateMovement;
        }
    }

    private void UpdateMovement()
    {
        baseStingForce = baseStingForce + (GameManager.Instance.GetStingForceModifier() / 10);
        forwardSpeed = forwardSpeed * GameManager.Instance.GetPlayerSpeedModifier();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * forwardSpeed);
            if (Input.GetMouseButton(0))
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * rotationspeed * Time.deltaTime, 0, Space.World);
                // rotate around local X
                transform.Rotate(-Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime, 0, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Target")
        {
            StingAttack(other.gameObject);
        }
    }

    private void StingAttack(GameObject other)
    {
        other.GetComponent<Rigidbody>().AddForce(Vector3.forward * baseStingForce, ForceMode.Impulse);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour, IGetStung
{
    [SerializeField] Animator targetAnimator;
    [SerializeField] bool canSting;
    [SerializeField] Transform childObjectTransform;
    [SerializeField] Transform childCanvasTransform;
    [SerializeField] Vector3 canvasAdjustment;
    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Collider triggerCollider;
    HornetController hornet;

    public Rigidbody GetMyRigidBody() => myRigidBody;

    private void Start()
    {
        canSting = true;
        hornet = FindObjectOfType<HornetController>();
    }

    private void Update()
    {
        childCanvasTransform.position = childObjectTransform.position + canvasAdjustment;
    }

    public void PlayExclamation()
    {
        targetAnimator.SetTrigger("TargetStung");
    }

    public void GetStung()
    {
        EventManager.Instance.TargetStung(this);
        canSting = false;
        PlayExclamation();
        childCanvasTransform.LookAt(Camera.main.transform);
        triggerCollider.enabled = false;
    }
}

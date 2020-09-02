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
    [SerializeField] Transform childCameraTargetTransform;
    [SerializeField] Vector3 canvasAdjustment;
    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Collider triggerCollider;

    public Rigidbody GetMyRigidBody() => myRigidBody;
    public Transform GetCameraTargetTransform => childCameraTargetTransform;

    private void Start()
    {
        canSting = true;
    }

    private void Update()
    {
        childCanvasTransform.position = childObjectTransform.position + canvasAdjustment;
        //this.transform.position = childObjectTransform.position;
        childCanvasTransform.LookAt(Camera.main.transform);

        childCameraTargetTransform.position = childObjectTransform.position;
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
        triggerCollider.enabled = false;
    }

}

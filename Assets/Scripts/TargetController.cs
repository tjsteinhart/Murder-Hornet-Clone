using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField] Image targetExclamation;
    [SerializeField] Animator targetAnimator;
    [SerializeField] bool canSting;
    [SerializeField] Collider targetCollider;

    public bool CanSting() => canSting;
    public Collider TargetCollider() => targetCollider;

    private void Start()
    {
        canSting = true;
    }

    public void SetCanSting(bool sting)
    {
        canSting = sting;
    }

    public void PlayExclamation()
    {
        targetAnimator.SetTrigger("TargetStung");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    [SerializeField] Image targetExclamation;
    [SerializeField] Animator targetAnimator;
    [SerializeField] bool canSting;

    public bool CanSting() => canSting;

    private void Start()
    {
        canSting = true;
        targetAnimator = GetComponent<Animator>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWaypoint : MonoBehaviour
{
    [SerializeField] Image targetingImage;
    [SerializeField] List<TargetController> targets;
    [SerializeField] Transform currentTarget;

    private void Start()
    {
        TargetManager targetManager = FindObjectOfType<TargetManager>();
        targets = targetManager.GetTargetList();
    }

    private void OnEnable()
    {
        EventManager.Instance.onTargetStung += ChangeTargetTransform;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onTargetStung -= ChangeTargetTransform;
        }
    }

    private void Update()
    {
        if(currentTarget != null)
        {
            targetingImage.transform.position = Camera.main.WorldToScreenPoint(currentTarget.position);
        }
        else
        {
            ChooseFirstTarget();
        }
    }

    public void ChooseFirstTarget()
    {
        currentTarget = targets[0].transform;
    }

    private void ChangeTargetTransform(TargetController target)
    {
        targets.Remove(target);
        currentTarget = targets[0].transform;
    }
}

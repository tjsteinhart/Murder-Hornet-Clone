using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWaypoint : MonoBehaviour
{
    [SerializeField] Image targetingImage;
    [SerializeField] List<TargetController> targets;
    [SerializeField] Transform currentTarget;
    [SerializeField] Vector3 targetingImageAdjustments;

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
            float minX = targetingImage.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = targetingImage.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(currentTarget.position + Vector3.up);

            if(Vector3.Dot((currentTarget.position + Vector3.up) - transform.position, transform.forward) < 0)
            {
                //Target behind the player
                if(pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            targetingImage.transform.position = pos;

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
        if(targets.Count > 0)
        {
            currentTarget = targets[0].transform;
        }
        else
        {
            targetingImage.gameObject.SetActive(false);
        }
    }
}

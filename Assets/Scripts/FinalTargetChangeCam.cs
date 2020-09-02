using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalTargetChangeCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    Quaternion currentRot;
    bool followingTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        followCam = GetComponent<CinemachineVirtualCamera>();
        followingTarget = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onFinalHit += ChangeCamToFinalTarget;
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.onFinalHit -= ChangeCamToFinalTarget;
        }
    }

    private void Update()
    {
        if (followingTarget)
        {
            followCam.transform.rotation = currentRot;
        }
    }

    private void ChangeCamToFinalTarget(Transform target)
    {
        currentRot = followCam.transform.rotation;
        followingTarget = true;
        followCam.Follow = target;
        followCam.LookAt = target;
    }
}

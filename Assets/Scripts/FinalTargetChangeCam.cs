using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalTargetChangeCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;

    // Start is called before the first frame update
    void Start()
    {
        followCam = GetComponent<CinemachineVirtualCamera>();
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

    private void ChangeCamToFinalTarget(Transform target)
    {
        followCam.Follow = target;
        followCam.LookAt = target;
    }
}

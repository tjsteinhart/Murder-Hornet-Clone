using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartController : MonoBehaviour
{
    [SerializeField] GameObject nest;
    [SerializeField] GameObject nestTree;
    [SerializeField] ParticleSystem leaveNestFX;
    [SerializeField] HornetController hornet;
    // Start is called before the first frame update
    void Start()
    {
        hornet = GetComponentInChildren<HornetController>();
    }

    private void OnEnable()
    {
        StartCoroutine(SubscribeToEvents());
    }

    IEnumerator SubscribeToEvents()
    {
        yield return new WaitForEndOfFrame();
        EventManager.Instance.onStartGameplay += HideNest;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= HideNest;
        }
    }

    public void HideNest()
    {
        nest.SetActive(false);
        nestTree.SetActive(false);
        hornet.UpdateMovement();
        hornet.SetIsMoving(true);
        leaveNestFX.Play();
    }

}

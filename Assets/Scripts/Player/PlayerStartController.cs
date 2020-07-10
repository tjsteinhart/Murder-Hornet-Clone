using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartController : MonoBehaviour
{
    [SerializeField] GameObject nest;
    [SerializeField] GameObject nestTree;
    [SerializeField] ParticleSystem leaveNestFX;
    [SerializeField] GameObject hornet;
    // Start is called before the first frame update
    void Start()
    {
        hornet.SetActive(false);
    }

    private void OnEnable()
    {
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
        hornet.SetActive(true);
        leaveNestFX.Play();
    }

}

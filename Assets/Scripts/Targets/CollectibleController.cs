using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] Animator collectibleAnimator;
    [SerializeField] GameObject keyObj;
    [SerializeField] ParticleSystem collectedFX;

    void Start()
    {
        collectibleAnimator.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += StartCollectibleAnim;
        EventManager.Instance.onEndGamePlay += StopCollectibleAnim;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= StartCollectibleAnim;
            EventManager.Instance.onEndGamePlay -= StopCollectibleAnim;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HornetController>())
        {
            EventManager.Instance.CollectibleHit();
            collectedFX.Play();
            keyObj.SetActive(false);
            Destroy(gameObject, .5f);
        }
    }

    private void StartCollectibleAnim()
    {
        collectibleAnimator.enabled = true;
    }

    private void StopCollectibleAnim()
    {
        collectibleAnimator.enabled = false;
    }
}

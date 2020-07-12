using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] Animator collectibleAnimator;

    void Start()
    {
        collectibleAnimator.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += StartCollectibleAnim;
        EventManager.Instance.onStartGameplay += StopCollectibleAnim;
    }

    private void OnDisable()
    {
        EventManager.Instance.onStartGameplay -= StartCollectibleAnim;
        EventManager.Instance.onStartGameplay -= StopCollectibleAnim;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HornetController>())
        {
            GameManager.Instance.IncrementCollectibleAmount(1);
            EventManager.Instance.CollectibleHit();
            Destroy(gameObject);
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

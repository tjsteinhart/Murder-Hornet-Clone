using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    UICollectiblesController collectiblesUI;
    [SerializeField] int collectiblesGathered;
    [SerializeField] ParticleSystem collectedFX;

    public int GetCollectiblesHit() => collectiblesGathered;

    private void Awake()
    {
        collectiblesUI = FindObjectOfType<UICollectiblesController>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += CheckCollectedAmount;
        EventManager.Instance.onFinalHit += CalculateCollected;
        EventManager.Instance.onCollectibleHit += HandleCollectibleHit;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= CheckCollectedAmount;
            EventManager.Instance.onFinalHit -= CalculateCollected;
            EventManager.Instance.onCollectibleHit += HandleCollectibleHit;
        }
    }

    public void CheckCollectedAmount()
    {
        collectiblesGathered = GameManager.Instance.GetCollectibleAmount();
    }

    public void HandleCollectibleHit(CollectibleController collectible)
    {
        ParticleSystem newCollectedFX = Instantiate(collectedFX, collectible.transform.position, Quaternion.identity, this.transform);
        collectiblesGathered++;
        collectiblesUI.CollectibleGridUpdate(collectiblesGathered - 1);
        Destroy(collectible.gameObject);
        Destroy(newCollectedFX, .5f);

    }

    public void CalculateCollected(Transform transform)
    {
        GameManager.Instance.IncrementCollectibleAmount(collectiblesGathered);
    }
}

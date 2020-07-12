using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectiblesController : MonoBehaviour
{

    [SerializeField] GameObject collectiblesGrid;
    [SerializeField] GameObject collectibleSlider;
    [SerializeField] List<Slider> collectibleSliders;
    bool collectibleGridOn = false;

    // Start is called before the first frame update
    void Start()
    {
        FillCollectibleGrid();
        collectiblesGrid.SetActive(false);
    }

    private void FillCollectibleGrid()
    {
        for (int i = 0; i < GameManager.Instance.GetMaxCollectibles(); i++)
        {
            GameObject newCollectible = Instantiate(collectibleSlider, collectiblesGrid.transform.position, Quaternion.identity, collectiblesGrid.transform);
            collectibleSliders.Add(newCollectible.GetComponent<Slider>());
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += ToggleCollectibleGrid;
        EventManager.Instance.onEndGamePlay += ToggleCollectibleGrid;
        EventManager.Instance.onCollectibleHit += CollectibleGridUpdate;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= ToggleCollectibleGrid;
            EventManager.Instance.onEndGamePlay -= ToggleCollectibleGrid;
            EventManager.Instance.onCollectibleHit -= CollectibleGridUpdate;
        }
    }

    private void ToggleCollectibleGrid()
    {
        collectibleGridOn = !collectibleGridOn;
        collectiblesGrid.SetActive(collectibleGridOn);
        StartCoroutine(AlreadyCollectedFill());
    }

    IEnumerator AlreadyCollectedFill()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < GameManager.Instance.GetCollectibleAmount(); i++)
        {
            collectibleSliders[i].fillRect.GetComponent<Image>().fillAmount = 1;
        }
    }


    public void CollectibleGridUpdate()
    {
        collectibleSliders[GameManager.Instance.GetCollectibleAmount() - 1].fillRect.GetComponent<Image>().fillAmount = 1;
    }

}

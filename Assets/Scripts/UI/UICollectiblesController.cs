using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= ToggleCollectibleGrid;
            EventManager.Instance.onEndGamePlay -= ToggleCollectibleGrid;
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


    public void CollectibleGridUpdate(int index)
    {
        if(index < collectibleSliders.Count)
        {
            collectibleSliders[index].fillRect.GetComponent<Image>().fillAmount = 1;
        }
        else
        {
            return;
        }
    }

}

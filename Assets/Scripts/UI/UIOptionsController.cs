using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRubyAmountText;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject collectiblesGrid;
    [SerializeField] GameObject collectibleSlider;
    [SerializeField] List<Slider> collectibleSliders;
    [SerializeField] int collectiblesIndex = 0;

    bool gameplayUIOn = true;
    //[SerializeField] Image settingsScreen;

    void Start()
    {
        UpdateRubyAmount();
        ToggleGameplayUI();
        
        for(int i = 0; i < GameManager.Instance.GetMaxCollectibles(); i++)
        {
            GameObject newCollectible = Instantiate(collectibleSlider, collectiblesGrid.transform.position, Quaternion.identity, collectiblesGrid.transform);
            collectibleSliders.Add(newCollectible.GetComponent<Slider>());
            if(i < GameManager.Instance.GetCollectibleAmount())
            {
                collectibleSliders[i].fillRect.GetComponent<Image>().fillAmount = 1;
                collectiblesIndex++;
            }
        }
        //settingsScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += ToggleGameplayUI;
        EventManager.Instance.onEndGamePlay += ToggleGameplayUI;
        EventManager.Instance.onCollectibleHit += CollectibleUIUpdate;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= ToggleGameplayUI;
            EventManager.Instance.onEndGamePlay -= ToggleGameplayUI;
            EventManager.Instance.onCollectibleHit -= CollectibleUIUpdate;

        }
    }

    void Update()
    {
        UpdateRubyAmount();
    }

    public void UpdateRubyAmount()
    {
        currentRubyAmountText.text = GameManager.Instance.GetRubyAmount().ToString();
    }

    void ToggleGameplayUI()
    {
        gameplayUIOn = !gameplayUIOn;
        retryButton.SetActive(gameplayUIOn);
        collectiblesGrid.SetActive(gameplayUIOn);
    }

    public void ShowOptionsScreen()
    {
        Time.timeScale = 0;
        //settingsScreen.gameObject.SetActive(true);
    }

    public void ExitOptionsScreen()
    {
        //settingsScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneLoader.Instance.RestartLevel();
    }

    public void CollectibleUIUpdate()
    {

    } 
}

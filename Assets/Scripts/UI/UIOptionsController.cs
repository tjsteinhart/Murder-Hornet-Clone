using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRubyAmountText;
    [SerializeField] GameObject retryButton;

    bool gameplayUIOn = true;
    //[SerializeField] Image settingsScreen;

    void Start()
    {
        UpdateRubyAmount();
        ToggleGameplayUI();


        //settingsScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += ToggleGameplayUI;
        EventManager.Instance.onEndGamePlay += ToggleGameplayUI;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= ToggleGameplayUI;
            EventManager.Instance.onEndGamePlay -= ToggleGameplayUI;

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

}

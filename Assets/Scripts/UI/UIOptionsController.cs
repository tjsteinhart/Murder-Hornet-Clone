using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRubyAmountText;
    [SerializeField] GameObject retryButton;
    [SerializeField] Toggle vibrateControlToggle;

    bool gameplayUIOn = true;
    [SerializeField] Image settingsScreen;

    void Start()
    {
        UpdateRubyAmount();
        ToggleGameplayUI();
        vibrateControlToggle.isOn = PlayerPrefsController.GetVibration() == 1 ? true : false;
        settingsScreen.gameObject.SetActive(false);
        UpdateVibration();
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
        TimeManager.Instance.PauseGame();
        settingsScreen.gameObject.SetActive(true);
    }

    public void UpdateVibration()
    {
        PlayerPrefsController.SetVibrate(vibrateControlToggle.isOn);
        GameManager.Instance.SetVibrateOption(vibrateControlToggle.isOn);
    }

    public void ExitOptionsScreen()
    {
        TimeManager.Instance.UnpauseGame();
        settingsScreen.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneLoader.Instance.RestartLevel();
    }

}

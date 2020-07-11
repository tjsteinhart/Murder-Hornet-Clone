using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRubyAmountText;
    [SerializeField] GameObject retryButton;

    bool retryButtonOn = true;
    //[SerializeField] Image settingsScreen;

    void Start()
    {
        UpdateRubyAmount();
        ToggleRetryButton();
        //settingsScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += ToggleRetryButton;
        EventManager.Instance.onEndGamePlay += ToggleRetryButton;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= ToggleRetryButton;
            EventManager.Instance.onEndGamePlay -= ToggleRetryButton;
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

    void ToggleRetryButton()
    {
        retryButtonOn = !retryButtonOn;
        retryButton.SetActive(retryButtonOn);
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

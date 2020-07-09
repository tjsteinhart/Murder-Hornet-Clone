using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIOptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentRubyAmountText;
    //[SerializeField] Image settingsScreen;

    void Start()
    {
        UpdateRubyAmount();
        //settingsScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateRubyAmount();
    }

    public void UpdateRubyAmount()
    {
        currentRubyAmountText.text = GameManager.Instance.GetRubyAmount().ToString();
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

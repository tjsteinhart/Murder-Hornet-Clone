using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEndController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelEndText;
    [SerializeField] TextMeshProUGUI rubiesEarnedText;
    [SerializeField] TargetManager targetManager;

    private void Start()
    {
        UpdateLevelNum();
    }

    private void OnEnable()
    {
        EventManager.Instance.onEndGamePlay += UpdateRubiesEarnedUI;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onEndGamePlay -= UpdateRubiesEarnedUI;
        }
    }

    private void UpdateLevelNum()
    {
        levelEndText.text = "Level " + SceneLoader.Instance.GetCurrentSceneIndex().ToString();
    }

    private void UpdateRubiesEarnedUI()
    {
        rubiesEarnedText.text = "+" + targetManager.GetRubiesGainedPerLevel() + " Earned";
    }

    public void NextLevel()
    {
        SceneLoader.Instance.NextLevel();
    }
}

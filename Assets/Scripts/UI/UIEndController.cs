using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIEndController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelEndText;
    [SerializeField] TextMeshProUGUI rubiesEarnedText;
    [SerializeField] TargetManager targetManager;
    [SerializeField] FloaterController floaterPrefab;
    [SerializeField] Transform rubiesEarnedImageTransform;
    [SerializeField] Transform totalRubiesFromOptionsCanvas;

    public Transform GetRubiesEarnedTransform() => rubiesEarnedImageTransform;
    public Transform GetTotalRubiesTransform() => totalRubiesFromOptionsCanvas;

    private void OnEnable()
    {
        UpdateLevelNum();
        UpdateRubiesEarnedUI();
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

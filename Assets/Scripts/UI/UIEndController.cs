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


    private void OnEnable()
    {
        UpdateLevelNum();
        UpdateRubiesEarnedUI();
        ProcessFloaters();
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

    public void ProcessFloaters()
    {
        SpawnFloaters(rubiesEarnedImageTransform.position, totalRubiesFromOptionsCanvas.position, targetManager.GetRubiesGainedPerLevel());
    }

    public void SpawnFloaters(Vector3 spawnPos, Vector3 targetPos, int floaterNum)
    {
        for(int i = 0; i < floaterNum; i++)
        {
            Vector3 randomSpawnPos = spawnPos + (Vector3)Random.insideUnitCircle;
            FloaterController floater = Instantiate(floaterPrefab, randomSpawnPos, Quaternion.identity, this.transform);
            floater.InitializeFloater(targetPos);
        }
        GameManager.Instance.IncrementRubyAmount(floaterNum);
    }
}

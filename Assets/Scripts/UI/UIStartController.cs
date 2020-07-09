using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStartController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedLevel;
    [SerializeField] TextMeshProUGUI speedCost;
    [SerializeField] TextMeshProUGUI stingLevel;
    [SerializeField] TextMeshProUGUI stingCost;
    [SerializeField] TextMeshProUGUI levelNum;

    private void Start()
    {
        UpdateSpeedButtonUI();
        UpdateStingButtonUI();
        UpdateLevelNum();
    }

    public void StartGameplay()
    {
        EventManager.Instance.StartGameplay();
    }

    public void UpgradeSpeed()
    {
        if(GameManager.Instance.GetRubyAmount() > GameManager.Instance.GetSpeedUpgradeCost())
        {
            GameManager.Instance.IncrementPlayerSpeedModifier(1);
        }
        UpdateSpeedButtonUI();
    }

    public void UpgradeSting()
    {
        if(GameManager.Instance.GetRubyAmount() > GameManager.Instance.GetStingUpgradeCost())
        {
            GameManager.Instance.IncrementPlayerStingModifier(1);
           
        }
        UpdateStingButtonUI();
    }

    public void UpdateSpeedButtonUI()
    {
        speedLevel.text = "Level " + GameManager.Instance.GetPlayerSpeedModifier().ToString();
        speedCost.text = GameManager.Instance.GetSpeedUpgradeCost().ToString();
    }

    public void UpdateStingButtonUI()
    {
        stingLevel.text = "Level " + GameManager.Instance.GetStingForceModifier().ToString();
        stingCost.text = GameManager.Instance.GetStingUpgradeCost().ToString();
    }

    public void UpdateLevelNum()
    {
        levelNum.text = "Level " + SceneLoader.Instance.GetCurrentSceneIndex().ToString();
    }

}

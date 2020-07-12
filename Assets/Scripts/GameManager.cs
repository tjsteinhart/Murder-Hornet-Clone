using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int playerSpeedIncrement = 0;
    [SerializeField] int playerStingIncrement = 0;
    [SerializeField] int rubyAmount = 0;
    [SerializeField] int speedUpgradeCost = 10;
    [SerializeField] int speedUpgradeCostModifier = 10;
    [SerializeField] int stingUpgradeCost = 10;
    [SerializeField] int stingUpgradeCostModifier = 15;

    [SerializeField] int rubiesPerTarget = 10;
    [SerializeField] int rubiesPerCollectedKeys = 100;

    [SerializeField] int collectibleAmount = 0;
    [SerializeField] int collectiblesNeeded = 3;
    bool enoughCollectibles;
    [SerializeField] bool vibrateOptionOn;

    public bool GetVibrateOption() => vibrateOptionOn;

    public int GetRubiesPerTarget() => rubiesPerTarget;
    public int GetRubesPerCollectedKeys() => rubiesPerCollectedKeys;
    public int GetRubyAmount() => rubyAmount;

    public int GetCollectibleAmount() => collectibleAmount;
    public int GetMaxCollectibles() => collectiblesNeeded;

    public int GetSpeedUpgradeCost() => speedUpgradeCost;
    public int GetStingUpgradeCost() => stingUpgradeCost;
    public int GetPlayerSpeedModifier() => playerSpeedIncrement;
    public int GetStingForceModifier() => playerStingIncrement;


    public void SetVibrateOption(bool isVibrateOn)
    {
        vibrateOptionOn = isVibrateOn;
    }

    #region Rubies
    public void IncrementRubyAmount(int value)
    {
        rubyAmount += value;
    }
    #endregion

    #region Collectibles
    public void IncrementCollectibleAmount(int value)
    {
        collectibleAmount += value;
    }

    public bool EnoughCollectibles()
    {
        if (collectibleAmount >= 3)
        {
            enoughCollectibles = true;
        }
        else
        {
            enoughCollectibles = false;
        }
        return enoughCollectibles;
    }
    #endregion

    #region Hornet Modifiers
    public void IncrementPlayerSpeedModifier(int value)
    {
        rubyAmount -= speedUpgradeCost;
        playerSpeedIncrement += value;
        speedUpgradeCost += speedUpgradeCostModifier;
    }

    public void IncrementPlayerStingModifier(int value)
    {
        rubyAmount -= stingUpgradeCost;
        playerStingIncrement += value;
        stingUpgradeCost += stingUpgradeCostModifier;
    }
    #endregion


}

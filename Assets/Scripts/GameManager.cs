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

    [SerializeField] int collectibleAmount = 0;
    [SerializeField] int collectiblesNeeded = 3;
    bool enoughCollectibles;
    bool vibrateOptionOn;

    public bool GetVibrateOption() => vibrateOptionOn;
    public void SetVibrateOption(bool isVibrateOn)
    {
        vibrateOptionOn = isVibrateOn;
    }

    #region Rubies
    public int GetRubyAmount()
    {
        return rubyAmount;
    }

    public void IncrementRubyAmount(int value)
    {
        rubyAmount += value;
    }
    #endregion

    #region Collectibles
    public int GetCollectibleAmount()
    {
        return collectibleAmount;
    }

    public void IncrementCollectibleAmount(int value)
    {
        collectibleAmount += value;
    }

    public int GetMaxCollectibles()
    {
        return collectiblesNeeded;
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
    public int GetSpeedUpgradeCost()
    {
        return speedUpgradeCost;
    }

    public int GetStingUpgradeCost()
    {
        return stingUpgradeCost;
    }

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

    public int GetPlayerSpeedModifier()
    {
        return playerSpeedIncrement;
    }

    public int GetStingForceModifier()
    {
        return playerStingIncrement;
    }
    #endregion


}

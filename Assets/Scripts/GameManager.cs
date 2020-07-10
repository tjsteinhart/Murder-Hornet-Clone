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

    [SerializeField] List<GameObject> managerList;

    private void Start()
    {
        foreach(GameObject manager in managerList)
        {
            if(manager != null)
            {
                return;
            }

            GameObject newManager = Instantiate(manager, transform.position, Quaternion.identity, this.transform);
        }
    }

    public int GetRubyAmount()
    {
        return rubyAmount;
    }

    public void IncrementRubyAmount(int value)
    {
        rubyAmount += value;
    }

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

    

}

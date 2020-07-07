using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int playerSpeedModifier = 1;
    [SerializeField] int playerStingForceModifier = 2;
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
        playerSpeedModifier += value;
        speedUpgradeCost += speedUpgradeCostModifier;
    }

    public void IncrementPlayerStingModifier(int value)
    {
        rubyAmount -= stingUpgradeCost;
        playerStingForceModifier += value * 2;
        stingUpgradeCost += stingUpgradeCostModifier;
    }

    public int GetPlayerSpeedModifier()
    {
        return playerSpeedModifier;
    }

    public int GetStingForceModifier()
    {
        return playerStingForceModifier;
    }

    

}

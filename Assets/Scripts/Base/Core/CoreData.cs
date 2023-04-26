using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "CoreData", fileName = "CoreData")]
public class CoreData : ScriptableObject
{
    [SerializeField] private float startingEnergyStored = 0.0f;
    [SerializeField] private float energyMax = 50.0f;
    [SerializeField] private int maxLevel = 2;
    [SerializeField] List<int> levelUpCost;
    [SerializeField] List<float> pickupRange;
    [SerializeField] private float energyStored;

    [SerializeField] private int startingCoreLevel = 0;
    private int coreLevel;

    private void OnEnable()
    {
        energyStored = startingEnergyStored;
        coreLevel = startingCoreLevel;
    }

    public int getLevelUpCost(int level)
    {
        return levelUpCost[level];
    }

    public float getPickupRange(int level)
    {
        return pickupRange[level];
    }

    public float getEnergy()
    {
        return energyStored;
    }

    public float getMaxEnergy()
    {
        return energyMax;
    }

    public float addEnergy(float energy)
    {
        energyStored = Math.Min(energyStored + energy, energyMax);
        return energyStored;
    }

    public float removeEnergy(float energy)
    {
        if(energyStored - energy >= 0)
        {
            energyStored -= energy;
        }
        else
        {
            energyStored = 0;
        }
        return energyStored;
    }

    public float setEnergy(float energy)
    {
        energyStored = Math.Min(energy, energyMax);
        return energyStored;
    }

    public int getLevel()
    {
        return coreLevel;
    }

    public int levelUp()
    {
        if (coreLevel + 1 <= maxLevel)
        {
            coreLevel++;
        }
        return coreLevel;
    }
}

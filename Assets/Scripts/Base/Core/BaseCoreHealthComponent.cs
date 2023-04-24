using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoreHealthComponent : HealthComponent
{
    [SerializeField] CoreData coreDataRef;

    override public void DamageStat(Stats statToDamage, float amount)
    {
        switch (statToDamage)
        {
            case Stats.ENERGY:
                coreDataRef.removeEnergy(amount);

                base.invokeReponse(amount);
                return;

            default: 
                return;
        }
    }

    override public void SetStat(Stats statToDamage, float value)
    {
        switch (statToDamage)
        {
            case Stats.ENERGY:
                coreDataRef.setEnergy(value);

                invokeReponse(value);
                return;

            default:
                return;
        }
    }

    override public List<float> GetStat(Stats stat)
    {
        if (stat != Stats.ENERGY)
            return null;

        return new List<float> { coreDataRef.getEnergy() };
    }
    override public void RegCompToStatSystem()
    {
        gameObject.GetComponent<StatusSystem>().RegisterAIComponent(this, Stats.ENERGY);
    }
}

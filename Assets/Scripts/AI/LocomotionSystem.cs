using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will become an abstract class for both enemies and turrets to reference
public abstract class LocomotionSystem : MonoBehaviour, BaseAIComponent
{

    /// <summary>
    /// The default speed value. Should not be modified unless the default value is changing.
    /// Clamped between 0 and maxSpeed.
    /// </summary>
    [SerializeField] protected float baseSpeed;

    /// <summary>
    /// The effective speed at which to move the object. Clamped between 0 and maxSpeed
    /// </summary>
    [SerializeField] protected float effectiveSpeed;

    /// <summary>
    /// The maximum value to which baseSpeed and effectiveSpeed are capped. Cannot be less than 0.
    /// </summary>
    [SerializeField] protected float maxSpeed;

    /// <summary>
    /// No idea what this is. TODO I guess.
    /// </summary>
    [SerializeField] protected bool isDirectMovement;

    /// <summary>
    /// The target the locomotion system should move the object towards
    /// </summary>
    [System.NonSerialized] protected GameObject target = null;

    public abstract void Target();

    void Start()
    {
        RegCompToStatSystem();
        effectiveSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Target();
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void DamageStat(Stats statToDamage, float amount)
    {
        switch (statToDamage)
        {
            case Stats.SPEED:
                effectiveSpeed -= amount;
                effectiveSpeed = Mathf.Clamp(effectiveSpeed, 0.0f, maxSpeed);
                return;

            case Stats.MAXSPEED:
                maxSpeed -= amount;
                if(maxSpeed < 0.0f)
                {
                    maxSpeed = 0.0f;
                }
                return;

            case Stats.BASESPEED:
                baseSpeed -= amount;
                baseSpeed = Mathf.Clamp(baseSpeed, 0.0f, maxSpeed);
                return;

            default:
                return;
        }
    }

    public void SetStat(Stats statToSet, float value)
    {
        switch (statToSet)
        {
            case Stats.SPEED:
                effectiveSpeed = value;
                effectiveSpeed = Mathf.Clamp(effectiveSpeed, 0.0f, maxSpeed);
                return;

            case Stats.MAXSPEED:
                maxSpeed = value;
                if (maxSpeed < 0.0f)
                {
                    maxSpeed = 0.0f;
                }
                return;

            case Stats.BASESPEED:
                baseSpeed = value;
                baseSpeed = Mathf.Clamp(baseSpeed, 0.0f, maxSpeed);
                return;

            default:
                return;
        }
    }

    public List<float> GetStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.SPEED:
                return new List<float> { effectiveSpeed };

            case Stats.MAXSPEED:
                return new List<float> { maxSpeed };

            case Stats.BASESPEED:
                return new List<float> { baseSpeed };

            default:
                return null;
        }
    }

    public void RegCompToStatSystem()
    {
        GetComponent<StatusSystem>().RegisterAIComponent(this, Stats.BASESPEED, Stats.SPEED, Stats.MAXSPEED);
    }
}

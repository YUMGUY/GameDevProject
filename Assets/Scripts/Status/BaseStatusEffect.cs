using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base abstract class for all status effects to inherit from
public abstract class BaseStatusEffect : ScriptableObject
{
    public Stats statToEffect;
    public bool isStackable; // can targets have multiple of these statuses
    public bool isPercentageBased; // Switch to percent based damage instead of fixed value
    public bool isOwnerInflicting; // can this status effect effect the owner of it
    public bool propagateToChildren;

    // may be removed in the future if all status effects do
    // not require this
    public float amount;  

    public abstract void Apply( StatusSystem entityStatSysToEffect, 
                                BaseAIComponent compToEffect);
}

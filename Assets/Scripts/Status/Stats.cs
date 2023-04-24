using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Stats
{
    UNDEFINED, // this position is chosen since c# will default to 0 if something went wrong
    HEALTH,
    BASESPEED,
    SPEED,
    MAXSPEED,
    FIRERATE,
    PROJECTILE_SCALE,
    PROJECTILE_SPEED,
    PROJECTILE_LIFETIME,
    PROJECTILE_DAMAGE,
    ENERGY // Currently used for the core base's energy level
};

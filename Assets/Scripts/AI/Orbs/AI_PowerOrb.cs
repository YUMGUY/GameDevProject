using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PowerOrb</c> is a collectable that gravitates towards the collector once they
/// are within a certain range of each other.
/// </summary>
public class AI_PowerOrb : AI_Base
{
    /// <summary>
    /// The max distance from which the orbs should move towards the player
    /// </summary>
    [SerializeField] float radius = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Used to determine when to retarget
    void Update()
    {
        // If target moved out of range, untarget.
        // This stops the orbs from moving towards the player once they are no longer in range
        if(target != null && Vector3.Distance(transform.position, target.transform.position) > radius)
        {
            Retarget(null);
        }

        // If no target try to find one
        // This basically detects when the orbs should start moving towards the player
        if (target == null)
        {
            GameObject potentialTarget = findClosestTargetWithTag(targetTags);
            if (potentialTarget != null && Vector3.Distance(transform.position, potentialTarget.transform.position) < radius)
            {
                Retarget(potentialTarget);
            }
        }
    }
}

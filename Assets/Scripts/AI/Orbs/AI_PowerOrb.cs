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
    [SerializeField] float attractionRadius = 7;

    /// <summary>
    /// The core data scriptable object containing the players power level information.
    /// </summary>
    [SerializeField] CoreData playerCoreData;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Used to determine when to retarget locomotion component. Does not actually move the power orb.
    void Update()
    {
        // If target moved out of range, untarget.
        // This stops the orbs from moving towards the player once they are no longer in range
        if(target != null && Vector3.Distance(transform.position, target.transform.position) > attractionRadius)
        {
            Retarget(null);
        }

        // If no target try to find one
        // This basically detects when the orbs should start moving towards the player
        if (target == null)
        {
            GameObject potentialTarget = findClosestTargetWithTag(targetTags);
            if (potentialTarget != null && Vector3.Distance(transform.position, potentialTarget.transform.position) < attractionRadius)
            {
                Retarget(potentialTarget);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MultiTag collisionTags = collision.GetComponent<MultiTag>();
        if(collisionTags != null)
        {
            if(collisionTags.ContainsTag("Player"))
            {
                if(playerCoreData != null)
                {
                    playerCoreData.addEnergy(1.0f);
                }
                DestroyObject();
            }
            else if (collisionTags.ContainsTag("OrbHunter"))
            {
                OrbHunter orbHunter = collision.GetComponent<OrbHunter>();
                if(orbHunter != null)
                {
                    orbHunter.IncrementOrbs();
                }
                DestroyObject();
            }
        }
    }

    /// <summary>
    /// Contains the logic that should run when the power orb is collected (ex: increment counters,
    /// particles, sfx, destroy self, etc.
    /// </summary>
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

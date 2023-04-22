using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PowerOrb</c> is a collectable that gravitates towards the collector once they
/// are within a certain range of each other.
/// </summary>
public class PowerOrb : MonoBehaviour
{
    public float energyValue;
    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Power orb movment is handled by the CollectOrbs script attached to the BaseCore object.
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// Orbs are collected upon collision. This function contains the logic that should 
    /// run when an orb is collected
    /// </summary>
    /// <param name="collision">The other Collider involved in this collision</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MultiTag collisionTags = collision.GetComponent<MultiTag>();
        if(collisionTags != null)
        {
            // case: orb is collected by player
            if(collisionTags.ContainsTag("Player"))
            {
                // gets the script that contains a reference to the core data
                CollectOrbs collectOrbs = collision.GetComponent<CollectOrbs>();
                if(collectOrbs != null)
                {
                    // gets the core data
                    CoreData playerCoreData = collectOrbs.coreData;

                    if (playerCoreData != null)
                    {
                        // increments player's collected energy
                        playerCoreData.addEnergy(energyValue);
                    }
                }
                DestroyObject();
            }

            // case: orb is collected by orb hunter enemy
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
    /// Contains the logic that should run when the power orb is destroyed
    /// </summary>
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

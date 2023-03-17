using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>OrbHunter</c> is an enemy that switches between 2 main types of behavior:
/// OrbHunting and PlayerHunting. 
/// <para>
/// The enemy starts off in OrbHunting mode. In this mode, these enemies priortize collecting 
/// power orbs. If there are orbs on the map. they will move towards orbs and absorb them in 
/// a way similar to the player's base. Otherwise, they will slowly approach the player. After
/// this enemy collects enough orbs, it transitions to PlayerHunting mode. In this mode, the 
/// enemy will rush the player extremely quickly dealing large amounts of damage if it manages 
/// to hit them. 
/// </para>
///
/// <para>
/// Killing this enemy in OrbHunting mode will cause it to drop all orbs it had absorbed in 
/// addition to the regular rewards for killing an enemy. Killing this enemy in PlayerHunting mode
/// will cause it to drop twice as many orbs as it had absorbed in addition to the regular rewards
/// for defeating an enemy.
/// </para>
/// </summary>
public class OrbHunter : AI_Base
{
    /// <summary> 
    /// The number of orbs this enemy needs to collect to transition from
    /// orbHunting to PlayerHunting mode.
    /// </summary>
    [SerializeField] int orbThreshold = 5;

    /// <summary> 
    /// The number of orbs this enemy has currently collected.
    /// </summary>
    [SerializeField] int collectedOrbs = 0;

    // Start is called before the first frame update
    void Start()
    {
        Retarget(findClosestTargetWithTag(targetTags));
    }

    // Used to determine when to retarget
    void Update()
    {
        // if no target try to find one
        if (target == null)
        {
            Retarget(findClosestTargetWithTag(targetTags));            
        }
    }

    /// <summary>
    /// Increments the collected orbs. If collected orbs equal or exceed the threshold, also calls SwitchToPlayerHuntingMode
    /// </summary>
    public void IncrementOrbs()
    {
        collectedOrbs++;
        if (collectedOrbs >= orbThreshold)
        {
            SwitchToPlayerHuntingMode();
        }
    }

    /// <summary>
    /// <para>
    /// Should be called when the conditions to switch modes are satisifed
    /// </para>
    /// <para>
    /// Handles all logic associated with the change from OrbHunting to PlayerHunting mode
    /// such as updating targetTags, updating the speed stat, playing particle and sound effects, etc.
    /// </para>
    /// </summary>
    void SwitchToPlayerHuntingMode()
    {
        // change target candidates from orbs to the player
        targetTags[0] = "Player";

        // increase speed of enemy
        locomotionSystem.SetStat(Stats.MAXSPEED, locomotionSystem.GetStat(Stats.MAXSPEED) * 2);
        locomotionSystem.SetStat(Stats.BASESPEED, locomotionSystem.GetStat(Stats.BASESPEED) * 2);
        locomotionSystem.SetStat(Stats.SPEED, locomotionSystem.GetStat(Stats.SPEED) * 2);

        // VFX/SFX go here eventually
        // TODO
    }

    /// <summary>
    /// <para>
    /// Contains the logic that should run when the enemy dies (ex: drop orbs, sound effects,
    /// particles, destroy self, etc.
    /// </para>
    /// <para>
    /// Killing this enemy in OrbHunting mode will cause it to drop all orbs it had absorbed in 
    /// addition to the regular rewards for killing an enemy. Killing this enemy in PlayerHunting mode
    /// will cause it to drop twice as many orbs as it had absorbed in addition to the regular rewards
    /// for defeating an enemy.
    /// </para>
    /// </summary>
    public void DestroyObject()
    {
        // Drop orbs
        if(orbSpawner != null)
        {
            // Drop default death orb
            orbSpawner.SpawnOrb();

            // Died in OrbHunting mode. Also drop collected orbs
            if (collectedOrbs < orbThreshold)
            {
                orbSpawner.SpawnOrbsInRadius(collectedOrbs, 2);
            }
            // Died in PlayerHunting mode. Also drop 2x collected orbs
            else
            {
                orbSpawner.SpawnOrbsInRadius(collectedOrbs * 2, 2);
            }
        }

        Destroy(gameObject);
    }
}

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
public class OrbHunter : MonoBehaviour
{
    /// <summary> 
    /// <c> orbLimit </c> is the number of orbs this enemy needs to collect to transition from
    /// orbHunting to PlayerHunting mode.
    /// </summary>
    [SerializeField] int orbLimit = 5;

    /// <summary> 
    /// <c> currentOrbs </c> is the number of orbs this enemy has currently collected.
    /// </summary>
    [SerializeField] int currentOrbs = 0;

    // TODO make enum instead?
    [SerializeField] bool currentMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

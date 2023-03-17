using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns power orbs. Mainly used by enemies/destructables to spawn
/// orbs on death/destruction.
/// </summary>
public class OrbSpawner : MonoBehaviour
{
    /// <summary>
    /// <para>
    /// The orb prefab to spawn.
    /// </para>
    /// <para>
    /// Default value is null. This is replaced with the orbPrefab in `Awake()`.
    /// Setting a value for this in editor will override this behavior and use that
    /// prefab instead.
    /// </para>
    /// </summary>
    [SerializeField] private GameObject orbPrefab = default;

    /// <summary>
    /// If the value of orbPrefab is null, attempts to set it to the orb prefab from the project resources.
    /// </summary>
    private void Awake()
    {
        if (orbPrefab == null)
        {
            orbPrefab = Resources.Load<GameObject>("Prefabs/power orb"); // Load the prefab from the Resources folder
        }
    }

    /// <summary>
    /// Spawns a single orb at the location of the gameObject
    /// </summary>
    public void SpawnOrb()
    {
        Instantiate(orbPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns the specified amount of orbs uniformly in a radius centered at the 
    /// location of gameObject
    /// </summary>
    /// <param name="numOrbsToSpawn">The number of power orbs that will be spawned</param>
    /// <param name="spawnRadius">Size of the spawn radius</param>
    public void SpawnOrbsInRadius(float numOrbsToSpawn, float spawnRadius)
    {
        for (int i = 0; i < numOrbsToSpawn; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Mathf.Sqrt(Random.value) * spawnRadius;
            Vector3 offset = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);
            Instantiate(orbPrefab, transform.position + offset, Quaternion.identity);
        }
    }
}

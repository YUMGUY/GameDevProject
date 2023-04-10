using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrbs : MonoBehaviour
{
    /// <summary>
    /// The max distance from which the orbs should move towards the player
    /// </summary>
    [SerializeField] float attractionRadius;

    /// <summary>
    /// Min speed at which orbs will move towards the base
    /// </summary>
    [SerializeField] float maxSpeed;

    /// <summary>
    /// Max speed at which orbs will move towards the base
    /// </summary>
    [SerializeField] float minSpeed;

    /// <summary>
    /// The custom animation curve to use for calculating the speed of the orb
    /// </summary>
    [SerializeField] AnimationCurve speedCurve;

    /// <summary>
    /// Tag used in the inspector to identify orb objects
    /// </summary>
    [SerializeField] string orbTag;

    /// <summary>
    /// Scriptable object that holds the player data
    /// </summary>
    [SerializeField] public CoreData coreData;

    /// <summary>
    /// Moves all the orbs closer to the base if within a certain radius. The speed the orbs will move towards
    /// the base is interpolated based on the distance from it. The closer they are, the closer they will be to
    /// maxSpeed and vice versa.
    /// </summary>
    void Update()
    {
        GameObject[] orbs = GameObject.FindGameObjectsWithTag(orbTag);
        foreach(GameObject orb in orbs)
        {
            // Calculate the distance between the orb and the player
            float distance = Vector3.Distance(orb.transform.position, transform.position);

            // if within affected distance, pull orb towards player
            if(distance < attractionRadius)
            {
                // Evaluate the custom animation curve to get the speed of the orb based on the distance
                float speed = Mathf.Lerp(minSpeed, maxSpeed, 1 - speedCurve.Evaluate(distance / attractionRadius));

                // Move the orb towards the player
                Vector3 direction = (transform.position - orb.transform.position).normalized;
                orb.transform.position = Vector3.MoveTowards(orb.transform.position, transform.position, speed * Time.deltaTime);
            }
        }
    }
}

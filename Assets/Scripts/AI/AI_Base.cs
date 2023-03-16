using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls and monitors all components related to the enemy this script
 * is embedded in
 */
public class AI_Base : MonoBehaviour
{
    [SerializeField] protected LocomotionSystem locomotionSystem;
    [SerializeField] protected AttackSystem_Base attackSystem;
    [SerializeField] protected string[] targetTags;

    protected GameObject target = null;

    /// <summary>
    /// Set the target for all AI components
    /// </summary>
    /// <param name="newTarget">The object that will be set as the target for the AI components</param>
    protected void Retarget(GameObject newTarget)
    {
        // find and send target to ai components
        target = newTarget;

        if (locomotionSystem)
        {
            locomotionSystem.setTarget(newTarget);
        }
        if (attackSystem)
        {
            attackSystem.setTarget(newTarget);
        }
    }

    private void Start()
    {
        Retarget(findClosestTargetWithTag(targetTags));
    }


    private void Update()
    {
        if (target == null)
        {
            Retarget(findClosestTargetWithTag(targetTags));
        }
    }


    /// <summary>
    /// Finds the closest GameObject with any of the provided tags in the scene
    /// </summary>
    /// <param name="tagsToFind">The tags for which matching GameObjects will be returned</param>
    /// <returns>
    /// The closest GameObject matching any of the tags in <c>tagstoFind</c>.
    /// If no GameObject matches the tags, <c>null is returned instead</c>
    /// </returns>
    protected GameObject findClosestTargetWithTag(string[] tagsToFind)
    {
        // find all with tags
        List<GameObject> targetList = findAllTargetsWithTagsList(tagsToFind);

        if (targetList.Count == 0)
            return null;

        // Find the closest object to target
        GameObject closestObj = targetList[0];
        Vector3 myPos = transform.position;
        float shortestDist = Vector3.Distance(myPos, closestObj.transform.position);

        // we can have the loop check the first index again, it's overhead is minimal
        foreach (GameObject currObjectToCheckDist in targetList)
        {
            float currObjDist = Vector3.Distance(myPos,
                                                 currObjectToCheckDist.transform.position);
            if (currObjDist < shortestDist)
            {
                shortestDist = currObjDist;
                closestObj = currObjectToCheckDist;
            }
        }

        return closestObj;
    }

    /// <summary>
    /// Finds all targets with specified tags within a scene
    /// </summary>
    /// <param name="tagsToFind">The tags for which matching GameObjects will be returned</param>
    /// <returns>
    /// A List of GameObjects containing all the objects matching any tags in <c>tagsToFind</c>. 
    /// If no matching GameObjects are found, an empty list will be returned.
    /// </returns>
    protected List<GameObject> findAllTargetsWithTagsList(string[] tagsToFind)
    {
        List<GameObject> targetList = new List<GameObject>();

        // Since there will never be too many turrets, I believe this is fine
        foreach (string currTag in tagsToFind)
        {
            targetList.AddRange(GameObject.FindGameObjectsWithTag(currTag));
        }

        //Debug.Log(targetList.ToString());

        return targetList;
    }
}

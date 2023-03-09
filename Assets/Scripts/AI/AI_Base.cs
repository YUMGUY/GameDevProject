using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls and monitors all components related to the enemy this script
 * is embedded in
 */
public class AI_Base : MonoBehaviour
{
    [SerializeField] LocomotionSystem locomotionSystem;
    [SerializeField] AttackSystem_Base attackSystem;
    [SerializeField] protected string[] targetTags;

    protected GameObject target = null;

    protected void Retarget()
    {

        // validate all components
        //checkComponentsAreValid();

        // find and send target to ai components
        target = findClosestTargetWithTag(targetTags);

        if (locomotionSystem)
        {
            locomotionSystem.setTarget(target);
        }
        if (attackSystem)
        {
            attackSystem.setTarget(target);
        }
    }

    private void Start()
    {
        Retarget();
    }


    private void Update()
    {
        if (target == null)
        {
            Retarget();
        }
    }


    // Helper functions
    // finds closest target within scene from enemy
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

    // Finds all targets with specified tags within scene
    protected List<GameObject> findAllTargetsWithTagsList(string[] tagsToFind)
    {
        List<GameObject> targetList = new List<GameObject>();

        // Since there will never be too many turrets, I believe this is fine
        foreach (string currTag in tagsToFind)
        {
            targetList.AddRange(GameObject.FindGameObjectsWithTag(currTag));
        }

        //Debug.Log(targetList.ToString());

        // Something went wrong if no targets were found
        if (targetList.Count == 0)
        {
            //Debug.Log(this.name.ToString() + ": unable to find target with tag");
            //this.enabled = false; // turn off the script so nothing weird happens
            return new List<GameObject>();
        }
        return targetList;
    }
}

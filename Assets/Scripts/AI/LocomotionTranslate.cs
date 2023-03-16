using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionTranslate : LocomotionSystem
{
    public override void Target()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, effectiveSpeed * Time.deltaTime);
    }
}

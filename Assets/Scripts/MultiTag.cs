using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component that allows assigning multiple tags to an object.
/// </summary>
public class MultiTag : MonoBehaviour
{
    [SerializeField] private List<string> tags;

    public List<string> Tags { get { return tags; } }

    public bool ContainsTag(string tag)
    {
        return tags.Contains(tag);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlertScriptable")]
public class AlertScriptable : ScriptableObject
{
    /// <summary>
    /// Color of the notification panel
    /// </summary>
    [SerializeField] public Color panelColor;

    /// <summary>
    /// The sound effect that will play when the alert is displayed
    /// </summary>
    [SerializeField] public AudioClip alertSound;

    /// <summary>
    /// How long the notification panel will stay on the screen before being removed.
    /// </summary>
    [SerializeField] public float duration;

    /// <summary>
    /// Color of the alert text
    /// </summary>
    [SerializeField] public Color textColor;

    /// <summary>
    /// The message that will be displayed to the player.
    /// </summary>
    [SerializeField][TextArea] public string message;
}

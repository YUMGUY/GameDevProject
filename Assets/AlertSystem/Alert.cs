using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Alert : MonoBehaviour
{
    [Header("UI Elements")]  
    /// <summary>
    /// The UI element that holds the text that will be displayed to the player.
    /// </summary>
    [SerializeField] private TextMeshProUGUI alertTextUI;

    [Header("Animation")]
    /// <summary>
    /// The UI transition animation used to make the notification appear (ex: fade in,
    /// slide in, etc). Plays in reverse when the notification is removed. 
    /// </summary>
    [SerializeField] private Animator alertAnimation;

    /// <summary>
    /// Used to track when an alert is being displayed. Used mainly to prevent bugs 
    /// from trying to play the display/remove alert animations too many times. While 
    /// true, no new alerts can be displayed.
    /// </summary>
    private bool isDisplayingAlert;

    [Header("SFX")]
    /// <summary>
    /// The sound effect that plays when the alert is displayed.
    /// </summary>
    [SerializeField] private AudioSource alertAudio;

    [Header("Message Customization")]
    /// <summary>
    /// How long the notification will stay on the screen before being removed.
    /// </summary>
    [SerializeField] float alertDuration;

    /// <summary>
    /// The message that will be displayed to the player.
    /// </summary>
    [SerializeField] [TextArea] private string alertMessage;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Code to execute when space key is pressed
            DisplayAlert();
        }
    }

    /// <summary>
    /// Plays the enter transition animation and displays the alert. Starts
    /// a coroutine timer to remove the alert when it expires.
    /// </summary>
    void DisplayAlert()
    {
        // Only attempt to display the alert if it is not currently being displayed
        if(!isDisplayingAlert)
        {
            isDisplayingAlert = true;
            alertTextUI.text = alertMessage;
            alertAnimation.Play("DisplayAlert");
            alertAudio.PlayOneShot(alertAudio.clip);
            StartCoroutine(WaitForAlertDuration());
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Coroutine timer that removes the alert when it expires.
    /// </summary>
    IEnumerator WaitForAlertDuration()
    {
        yield return new WaitForSeconds(alertDuration);
        RemoveAlert();
    }

    /// <summary>
    /// Plays the exit transition animation that removes the alert.
    /// </summary>
    void RemoveAlert()
    {
        alertAnimation.Play("RemoveAlert");
        StartCoroutine(WaitForRemoveAnimationDuration());
    }

    /// <summary>
    /// Waits until the exit transition animation has finished playing and sets
    /// isDisplayingAlert to false so the next alert can occur.
    /// </summary>
    IEnumerator WaitForRemoveAnimationDuration()
    {
        float removeAnimationDuration = alertAnimation.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(removeAnimationDuration);
        isDisplayingAlert = false;
    }

}

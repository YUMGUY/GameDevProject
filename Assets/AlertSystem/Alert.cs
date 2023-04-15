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
    [SerializeField] private TextMeshProUGUI notificationAlertTextUI;

    [Header("Animation")]
    /// <summary>
    /// The UI transition animation used to make the notification appear (ex: fade in,
    /// slide in, etc). Plays in reverse when the notification is removed. 
    /// </summary>
    [SerializeField] private Animator notificationAlertAnimation;

    /// <summary>
    /// The UI animation used to display the big warning message
    /// </summary>
    [SerializeField] private Animator bigWarningAlertAnimation;

    /// <summary>
    /// Used to track when an alert is being displayed. Used mainly to prevent bugs 
    /// from trying to play the display/remove alert animations/sfx too many times. 
    /// While true, no new alerts can be displayed.
    /// </summary>
    private bool isDisplayingNotificationAlert;

    /// <summary>
    /// Used to track when an alert is being displayed. Used mainly to prevent bugs 
    /// from trying to play the display/remove alert animations/sfx too many times. 
    /// While true, no new alerts can be displayed.
    /// </summary>
    private bool isDisplayingBigWarningAlert;

    [Header("SFX")]
    /// <summary>
    /// The sound effect that plays when the notification alert is displayed.
    /// </summary>
    [SerializeField] private AudioSource notificationAlertAudio;

    /// <summary>
    /// The sound effect that plays when the big warning alert is displayed.
    /// </summary>
    [SerializeField] private AudioSource bigWarningAlertAudio;

    [Header("Message Customization")]
    /// <summary>
    /// How long the notification will stay on the screen before being removed.
    /// </summary>
    [SerializeField] float notificationAlertDuration;

    /// <summary>
    /// The message that will be displayed to the player.
    /// </summary>
    [SerializeField] [TextArea] private string notificationAlertMessage;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Code to execute when space key is pressed
            DisplayNotificationAlert();
        }
        if (Input.GetKey(KeyCode.P))
        {
            // Code to execute when space key is pressed
            DisplayBigWarningAlert();
        }
    }

    /// <summary>
    /// Plays the enter transition animation and displays the alert. Starts
    /// a coroutine timer to remove the alert when it expires.
    /// </summary>
    void DisplayNotificationAlert()
    {
        // Only attempt to display the alert if it is not currently being displayed
        if(!isDisplayingNotificationAlert)
        {
            isDisplayingNotificationAlert = true;
            notificationAlertTextUI.text = notificationAlertMessage;
            notificationAlertAnimation.Play("DisplayAlert");
            notificationAlertAudio.PlayOneShot(notificationAlertAudio.clip);
            StartCoroutine(WaitForNotificationAlertDuration());
        }
    }

    /// <summary>
    /// Coroutine timer that removes the notification alert when it expires.
    /// </summary>
    IEnumerator WaitForNotificationAlertDuration()
    {
        yield return new WaitForSeconds(notificationAlertDuration);
        RemoveNotificationAlert();
    }

    /// <summary>
    /// Plays the exit transition animation that removes the alert.
    /// </summary>
    void RemoveNotificationAlert()
    {
        notificationAlertAnimation.Play("RemoveAlert");
        StartCoroutine(WaitForRemoveNotificationAlertAnimationDuration());
    }

    /// <summary>
    /// Waits until the exit transition animation has finished playing and sets
    /// isDisplayingNotificationAlert to false so the next alert can occur.
    /// </summary>
    IEnumerator WaitForRemoveNotificationAlertAnimationDuration()
    {
        float removeAnimationDuration = notificationAlertAnimation.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(removeAnimationDuration);
        isDisplayingNotificationAlert = false;
    }

    /// <summary>
    /// Plays the big warning alert animation
    /// </summary>
    void DisplayBigWarningAlert()
    {
        if (!isDisplayingBigWarningAlert)
        {
            isDisplayingBigWarningAlert = true;
            bigWarningAlertAnimation.Play("BigWarningAnimation");
            bigWarningAlertAudio.PlayOneShot(bigWarningAlertAudio.clip);
            StartCoroutine(WaitForBigWarningAlertDuration());
        }
    }

    /// <summary>
    /// Coroutine timer that removes the big warning alert when it expires.
    /// </summary>
    IEnumerator WaitForBigWarningAlertDuration()
    {
        // hardcoded big warning animation length because getting it programatically
        // did not always return the correct duration
        yield return new WaitForSeconds(4.5f);
        isDisplayingBigWarningAlert = false;
        bigWarningAlertAnimation.Play("Empty State");

    }

}

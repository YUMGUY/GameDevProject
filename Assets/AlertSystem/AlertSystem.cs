using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertSystem : MonoBehaviour
{
    [Header("UI Elements")]
    /// <summary>
    /// The UI element that holds the panel that will be displayed to the player.
    /// </summary>
    [SerializeField] private RawImage notificationAlertPanelUI;

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
    /// The audio source component that plays the notification alert sound.
    /// </summary>
    [SerializeField] private AudioSource notificationAlertAudio;

    /// <summary>
    /// The audio source component that plays the big warning alert sound.
    /// </summary>
    [SerializeField] private AudioSource bigWarningAlertAudio;

    [Header("Event")]
    /// <summary>
    /// The event instance that will be used to track when a notification alert
    /// should be displayed.
    /// </summary>
    private NotificationAlertEvent notificationAlertEvent = new NotificationAlertEvent();

    /// <summary>
    /// The event instance that will be used to track when a big warning alert
    /// should be displayed.
    /// </summary>
    private BigWarningAlertEvent bigWarningAlertEvent = new BigWarningAlertEvent();

    void Start()
    {
        // subscribe to the notification alert event so that the display notification
        // method is called whenever the event is invoked
        notificationAlertEvent.AddListener(DisplayNotificationAlert);

        // subscribe to the big warning alert event so that the display big warning
        // method is called whenever the event is invoked
        bigWarningAlertEvent.AddListener(DisplayBigWarningAlert);
    }

    /// <summary>
    /// Called by other objects/scripts to display a notifcation alert
    /// </summary>
    /// <param name="alertData">Scriptable object containing customizable data for the alert</param>
    public void SendNotificationAlert(AlertScriptable alertData)
    {
        print("invoked");
        notificationAlertEvent.Invoke(alertData);
    }

    /// <summary>
    /// Plays the enter transition animation and displays the alert. Starts
    /// a coroutine timer to remove the alert when it expires.
    /// </summary>
    void DisplayNotificationAlert(AlertScriptable alertData)
    {
        // Only attempt to display the alert if it is not currently being displayed
        if(!isDisplayingNotificationAlert)
        {
            isDisplayingNotificationAlert = true;
            notificationAlertPanelUI.color = alertData.panelColor;
            notificationAlertTextUI.color = alertData.textColor;
            notificationAlertTextUI.text = alertData.message;
            notificationAlertAnimation.Play("DisplayAlert");
            notificationAlertAudio.PlayOneShot(alertData.alertSound);
            StartCoroutine(WaitForNotificationAlertToExpire(alertData.duration));
        }
    }

    /// <summary>
    /// Coroutine timer that removes the notification alert when it expires.
    /// </summary>
    IEnumerator WaitForNotificationAlertToExpire(float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveNotificationAlert();
    }

    /// <summary>
    /// Plays the exit transition animation that removes the alert.
    /// </summary>
    void RemoveNotificationAlert()
    {
        notificationAlertAnimation.Play("RemoveAlert");
        StartCoroutine(WaitForRemoveNotificationAlertAnimationToFinish());
    }

    /// <summary>
    /// Waits until the exit transition animation has finished playing and sets
    /// isDisplayingNotificationAlert to false so the next alert can occur.
    /// </summary>
    IEnumerator WaitForRemoveNotificationAlertAnimationToFinish()
    {
        float removeAnimationDuration = notificationAlertAnimation.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(removeAnimationDuration);
        isDisplayingNotificationAlert = false;
    }

    /// <summary>
    /// Called by other objects/scripts to display a big warning alert
    /// </summary>
    public void SendBigWarningAlert()
    {
        bigWarningAlertEvent.Invoke();
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
            StartCoroutine(WaitForBigWarningAlertToExpire());
        }
    }

    /// <summary>
    /// Coroutine timer that removes the big warning alert when it expires.
    /// </summary>
    IEnumerator WaitForBigWarningAlertToExpire()
    {
        // Hardcoded big warning animation length because getting it programatically
        // sometimes returned the duration of the "Empty State" animation instead of
        // "BigWarningAnimation".
        yield return new WaitForSeconds(4.5f);
        isDisplayingBigWarningAlert = false;
        // This line needs to be here to reset the animation state or the animation will
        // only be able to play once
        bigWarningAlertAnimation.Play("Empty State");
    }

}

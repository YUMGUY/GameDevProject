using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTester : MonoBehaviour
{
    public AlertSystem alert;
    public AlertScriptable alertScriptable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            alert.SendNotificationAlert(alertScriptable);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            alert.SendBigWarningAlert();
        }
    }
}
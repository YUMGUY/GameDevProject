using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAlert : MonoBehaviour
{
    // Start is called before the first frame update
    public AlertScriptable[] alerts;
    public AlertSystem alertSys;
    public Map mapRef;

    [SerializeField]private string currentZone;
    private string zoneExtracted;
    private bool changedZone;
    void Start()
    {
        print("starting zone: " + mapRef.getCurrentZone().name);
        currentZone = mapRef.getCurrentZone().name;
        changedZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        zoneExtracted = mapRef.getCurrentZone().name;
        if (!zoneExtracted.Equals(currentZone) && !changedZone)
        {
            NotifyZoneChange(zoneExtracted);
            currentZone = zoneExtracted;
            changedZone = true;
        }
        else
        {
            changedZone = false;
        }
    }

    public void NotifyZoneChange(string zone)
    {
        print("changed zones");
        switch(zone)
        {
            case "zone1":
                alertSys.SendNotificationAlert(alerts[0]);
                break;
            case "zone2":
                alertSys.SendNotificationAlert(alerts[1]);
                break;
            case "zone3":
                print("zone 3 should be alerted");
                alertSys.SendNotificationAlert(alerts[2]);
                break;
            case "zone4":
                alertSys.SendNotificationAlert(alerts[3]);
                break;
            case "zone5":
                alertSys.SendNotificationAlert(alerts[4]);
                break;
        }
        
    }
}

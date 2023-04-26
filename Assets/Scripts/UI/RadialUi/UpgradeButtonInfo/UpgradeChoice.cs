using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeChoice : MonoBehaviour
{
    public RadialUi radialUIref;
    public Upgrade chosenUpgrade;
   

    private void Start()
    {
        if(chosenUpgrade != null)
        {
            //transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            gameObject.GetComponent<Image>().sprite = chosenUpgrade.icon;

            // FIXME: use GetChildrenComponent not explicit stuff like this...
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = chosenUpgrade.title;
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cost: " + chosenUpgrade.cost.ToString();
        }
       
    }
    public void ApplyUpgrade()
    {
       
        print("applied upgrade: " + chosenUpgrade.title);
        radialUIref.selectedGameObject.GetComponent<TurretUpgrade>().BuyUpgrade(chosenUpgrade);
    }
    public void RefreshUpdateScreen()
    {
        // clear upgrade screen first
        foreach (Transform child in radialUIref.UpgradeScreenPanel.transform)
        {
            if (child.name != "Close")
            {
                Destroy(child.gameObject);
            }

        }
        List<Upgrade> newUpgrades = radialUIref.selectedGameObject.GetComponent<TurretUpgrade>().GetBuyableUpgrades();
        // FIXME: code better positioning of choices
        float ypos = 375f;
        float xpos = 0;
        for (int i = 0; i < newUpgrades.Count; ++i)
        {
            if (ypos <= -325f)
            {
                ypos = 325f;
                xpos += 100;
            }
            // instantiate upgrade choice buttons prefab
            GameObject createdButton = Instantiate(radialUIref.upgradeButtonPrefab, radialUIref.UpgradeScreenPanel.transform);
            createdButton.transform.localPosition = new Vector3(xpos, ypos, 0);

            // FIXME: Figure out how to scale the icons properly
            createdButton.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
            UpgradeChoice button_Upgrade = createdButton.GetComponent<UpgradeChoice>();
            button_Upgrade.chosenUpgrade = newUpgrades[i];
            button_Upgrade.radialUIref = radialUIref;
            ypos -= 275f;
        }
    }
}

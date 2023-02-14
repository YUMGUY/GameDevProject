using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] Image icon;
	
	public void Set(UpgradeData upgradeData)
	{
		icon.sprite = upgradeData.icon;
	}
}

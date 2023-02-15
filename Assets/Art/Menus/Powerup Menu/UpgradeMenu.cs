// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class UpgradeMenu : MonoBehaviour
// {
    // public static bool GameIsPaused = false;
	
	// public GameObject upgradeMenuUI;

    // // Update is called once per frame
    // void Update()
    // {
		// /*Input for level-up goes in if once determined*/
        // if (Input.GetKeyDown(KeyCode.Escape))
		// {
			// if(GameIsPaused)
			// {
				// Resume();
			// } else
			// {
				// Pause();
			// }
		// }
	// }
	
	// void Pause ()
	// {
		// upgradeMenuUI.SetActive(true);
		// Time.timeScale = 0f;
		// GameIsPaused = true;
	// }
	
	// public void Upgrade1 ()
	// {
		// Debug.Log("Upgrade1");
		// upgradeMenuUI.SetActive(false);
		// Time.timeScale = 1f;
		// GameIsPaused = false;
	// }
	
	// public void Upgrade2()
	// {
		// Debug.Log("Upgrade2");
		// upgradeMenuUI.SetActive(false);
		// Time.timeScale = 1f;
		// GameIsPaused = false;
	// }
	
	// public void Upgrade3()
	// {
		// Debug.Log("Upgrade3");
		// upgradeMenuUI.SetActive(false);
		// Time.timeScale = 1f;
		// GameIsPaused = false;
	// }
// }
//
// public enum UpgradeType
// {
	// WeaponUpgrade,
	// ItemUpgrade,
	// WeaponUnlock,
	// ItemUnlock
// }

// [CreateAssetMenu]

// public class UpgradeMenu : ScriptableObject
// {
    // public UpgradeType upgradeType;
	// public string Name;
	// public Sprite icon;
// }

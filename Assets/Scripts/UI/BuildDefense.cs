using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDefense : MonoBehaviour
{
    public bool spawning;
    // Used for scripts that need to know if the action of clicking
    // to place a turret has just occured (ex: base movement script)
    public bool justSpawned; 

    public Transform parentBase;
    [SerializeField] Platform platform;
    public GameObject objectSpawned;
    public GameObject defense;
    [SerializeField] Map map;
    [SerializeField] AlertSystem alertSystem;

    private Tower towerBase;
    public SpriteRenderer defenseSprite;
    [SerializeField] private int numberOfClicks = 0;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip moveTurretSFX;

    void Update()
    {
        if (!spawning)
            return;

        if (Input.GetMouseButton(0))
        {
            Tuple<Vector3, bool> placement = towerBase.place(defense);
            if (placement.Item2) //Placed successfully
            {
                numberOfClicks = 1;
                justSpawned = true;
                defense.GetComponent<AI_Base>().enabled = true; //Turn the AI on once placed
                defenseSprite.color = new Color(1f, 1f, 1f, 1f);
                spawning = false;
                // play sound effect
                if (moveTurretSFX != null)
                {
                    audioSource?.PlayOneShot(moveTurretSFX);
                }
            }
        } else if (Input.GetMouseButton(1))
        {
            spawning = false;
            defense.GetComponent<TurretUpgrade>().RefundBasic();
            Destroy(defense);
        }
    }

    private void LateUpdate()
    {
        if(defense != null && numberOfClicks == 0)
        {
            defenseSprite.color = new Color(1, 1, 1, .5f);
            spawning = true;
        }
    }

    public void BuyDefense()
    {
        if (!spawning)
        {
            defense = Instantiate(objectSpawned);

            if (!defense.activeInHierarchy)
            {
                // Failed to buy
                spawning = false;
                Destroy(defense);
                return;
            }

            towerBase = defense.AddComponent<Tower>();
            towerBase.parent = parentBase.gameObject;
            towerBase.platform = platform;
            defense.transform.parent = towerBase.getTowerBase().transform;

            defense.GetComponent<AI_Base>().enabled = false; //Turn off the AI while placing

            defenseSprite = defense.GetComponentInChildren<SpriteRenderer>();
            defense.GetComponent<TurretUpgrade>().map = map;
            defense.GetComponent<TurretUpgrade>().alertSystem = alertSystem;
            numberOfClicks = 0;
        }
    }

    public void MoveDefense(GameObject def)
    {
        defense = def;
        towerBase = defense.GetComponent<Tower>();
        towerBase.setPlacing(true);
        defenseSprite = defense.GetComponentInChildren<SpriteRenderer>();
        numberOfClicks = 0;
    }
}

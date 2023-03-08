using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialUi : MonoBehaviour
{
    [SerializeField] GameObject createButton;
    [SerializeField] GameObject upgradeButton;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject deleteButton;
    [SerializeField] Platform platform;

    private bool open;

    private void Start()
    {
        createButton.GetComponent<CanvasGroup>().alpha = 0.0f;
        upgradeButton.GetComponent<CanvasGroup>().alpha = 0.0f;
        moveButton.GetComponent<CanvasGroup>().alpha = 0.0f;
        deleteButton.GetComponent<CanvasGroup>().alpha = 0.0f;

        disableInteraction();

        open = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && open)
        {
            setAlpha(0.0f);
            open = false;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.transform.position = Input.mousePosition;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (platform.pointInBase(cursorPos))
            {
                platform.setCursorPos(cursorPos);
                Tuple<Vector2, float, int> snap = platform.getSnap();
                gameObject.transform.position = Camera.main.WorldToScreenPoint(snap.Item1);

                bool onTower = platform.towerExists();
                createButton.GetComponent<Button>().interactable = !onTower;
                upgradeButton.GetComponent<Button>().interactable = onTower;
                moveButton.GetComponent<Button>().interactable = onTower;
                deleteButton.GetComponent<Button>().interactable = onTower;
            }
            else
            {
                createButton.GetComponent<Button>().interactable = true;
                upgradeButton.GetComponent<Button>().interactable = false;
                moveButton.GetComponent<Button>().interactable = false;
                deleteButton.GetComponent<Button>().interactable = false;
            }

            setAlpha(1.0f);
            open = true;
        }
    }

    public void setAlpha(float alpha)
    {
        createButton.GetComponent<CanvasGroup>().alpha = alpha;
        upgradeButton.GetComponent<CanvasGroup>().alpha = alpha;
        moveButton.GetComponent<CanvasGroup>().alpha = alpha;
        deleteButton.GetComponent<CanvasGroup>().alpha = alpha;
    }

    public void disableInteraction()
    {
        createButton.GetComponent<Button>().interactable = false;
        upgradeButton.GetComponent<Button>().interactable = false;
        moveButton.GetComponent<Button>().interactable = false;
        deleteButton.GetComponent<Button>().interactable = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPopUp : MonoBehaviour
{
    public GameObject MinimapDrawing;

    // Start is called before the first frame update
    void Start()
    {
        MinimapDrawing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")) {
            MinimapDrawing.SetActive(!MinimapDrawing.activeSelf);
        }
    }
}

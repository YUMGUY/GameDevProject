using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    float currentTime;
    [SerializeField] int startMinutes;
    public TextMeshProUGUI currentTimeText;
    
    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        currentTimeText.text = time.ToString(@"mm\:ss");
    }


}

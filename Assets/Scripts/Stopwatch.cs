using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    float currentTime = 0;

    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        currentTimeText.text = time.ToString(@"mm\:ss");
    }

    // Returns the current time of the timer
    public float getCurrentTime()
    {
        return currentTime;
    }
}

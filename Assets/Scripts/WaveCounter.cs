using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCounter : MonoBehaviour
{
    public TextMeshPro currentWaveText;
    int currentWave;

    public void setWave(int waveToSet)
    {
        currentWave = waveToSet;
        currentWaveText.text = "Wave: " + currentWave.ToString();
    }
}

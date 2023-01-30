using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for interfacing with the system's subobjects (timer, wave number display, ect)

public class WaveUISystem : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public TextMeshProUGUI waveCounter;

    // Start is called before the first frame update
    void Start()
    {
        waveCounter.GetComponent<WaveCounter>().setWave(5);
    }
}

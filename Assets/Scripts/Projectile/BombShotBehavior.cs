using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShotBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBombExplosion()
    {
        GameObject explosionEffect = Instantiate(explosion);
        explosionEffect.transform.position = transform.position;
        explosionEffect.transform.rotation = Quaternion.identity;
    }
}

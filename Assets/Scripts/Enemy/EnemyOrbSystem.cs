using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbSystem : MonoBehaviour
{
    // Start is called before the first frame update
    // only drop one orb
    public Map map;
    private bool dropped = false;

    private void Start()
    {
        animController = GetComponent<Animator>();
    }
    public GameObject orb;
    [SerializeField] private Animator animController;
    public void DropOrb()
    {
        if(dropped == false)
        {
            GameObject drop = Instantiate(orb);
            drop.transform.position = transform.position;
            drop.GetComponent<PowerOrb>().map = map;

            animController.SetTrigger("death");
            dropped = true;
        }
       
    }

    // Called in the enemy explosion animation as an animation event
    public void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float BulletSpeed = 20;
    public int damageAmount = 20;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        PlaterSwordA redenemyswordr = collision.GetComponent<PlaterSwordA>();

        if ( redenemyswordr != null )
        {
            redenemyswordr.TakeDamage(damageAmount);
        }

        PlayerM playerm = collision.GetComponent<PlayerM>();

        if ( playerm != null )
        {
            playerm.TakeDamage(damageAmount);
        }
    }
}

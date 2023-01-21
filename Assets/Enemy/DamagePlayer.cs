using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 20;

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

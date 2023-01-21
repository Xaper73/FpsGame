using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnTriggerEnter(Collider collision)
    {
        EnemySwordA enemysworda = collision.GetComponent<EnemySwordA>();

        if ( enemysworda != null )
        {
            enemysworda.TakeDamage(damageAmount);
        }
    }
}

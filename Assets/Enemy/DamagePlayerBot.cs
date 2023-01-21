using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerBot : MonoBehaviour
{
    public int damageAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlaterSwordA>().TakeDamage(damageAmount);
        }
    }
}

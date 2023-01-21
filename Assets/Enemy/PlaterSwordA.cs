using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class PlaterSwordA : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
//    Transform player;
    public int HP = 100;
    public Animator animator;
    public Slider healthBar;
    public float AgroDistance = 3;
    public float PointDistanse = 1;
    public float AttackDistance = 2;
    public int damage;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public PointA A;
    public PointB B;
    public PointC C;
    public Collider weapon;
    public GameObject Cursor;
    public GameObject Blood;
    public Transform bloodPoint;
    public float timeBtwShors;
    public float startTimeBtwShors;

    public Transform[] targets;
    public GameObject[] player;

    private void Start()
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        weapon.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        
        targets = new Transform[player.Length];

        for (int i = 0; i < player.Length; i ++)
        {
            targets[i] = player[i].GetComponent<Transform>();
        }

        healthBar.value = HP;

        if (HP > 0)
        {
            if (Vector3.Distance(transform.position, player.position) < AgroDistance)
            {
                transform.LookAt(player);
                agent.SetDestination(player.position);
                float distance = Vector3.Distance(transform.position, player.position);
                agent.isStopped = false;
                animator.SetBool("idle", false);
                animator.SetBool("run", true);
                animator.SetBool("attack", false);
            }
            if ((Vector3.Distance(transform.position, player.position) < AgroDistance) && (Vector3.Distance(transform.position, player.position) < AttackDistance))
            {
                if (timeBtwShors <= 0)
                {
                    animator.SetBool("idle", false);
                    animator.SetBool("run", false);
                    animator.SetBool("attack", true);
                    agent.isStopped = true;
                    transform.LookAt(player);
                    weapon.GetComponent<Collider>().enabled = true;
                    Invoke("ColliderWeapon", 0.6f);
                    timeBtwShors = startTimeBtwShors;
                }
                else
                {
                    agent.isStopped = true;
                    transform.LookAt(player);
                    animator.SetBool("idle", true);
                    animator.SetBool("run", false);
                    animator.SetBool("attack", false);
                    timeBtwShors -= Time.deltaTime;
                } 
            }
            if (Vector3.Distance(transform.position, player.position) > AgroDistance)              
            { 
                if (A.statusPoint == "" || A.statusPoint == "ZaxvatBlue" || A.statusPoint == "ZaxvatRed") 
                {
                    if(Vector3.Distance(transform.position, pointA.position) < PointDistanse)
                    {
                        agent.isStopped = true;
                        animator.SetBool("idle", true);
                        animator.SetBool("run", false);
                        animator.SetBool("attack", false);
                    }
                    else if (A.statusPoint != "FullZaxvatRed" || A.statusPoint != "FullZaxvatBlue")
                    {
                        agent.SetDestination(pointA.position);
                        float distance = Vector3.Distance(transform.position, pointA.position);
                        animator.SetBool("idle", false);
                        animator.SetBool("run", true);
                        animator.SetBool("attack", false);
                    }
                }
                if (A.statusPoint == "FullZaxvatRed"  || A.statusPoint == "FullZaxvatBlue")
                {
                    if(Vector3.Distance(transform.position, pointB.position) < PointDistanse)
                    {
                        agent.isStopped = true;
                        animator.SetBool("idle", true);
                        animator.SetBool("run", false);
                        animator.SetBool("attack", false);
                    }
                    else if (B.statusPoint == "" || B.statusPoint == "ZaxvatBlue" || B.statusPoint == "ZaxvatRed")
                    {
                        agent.isStopped = false;
                        agent.SetDestination(pointB.position);
                        float distance = Vector3.Distance(transform.position, pointB.position);
                        animator.SetBool("idle", false);
                        animator.SetBool("run", true);
                        animator.SetBool("attack", false);
                    }
                }
                if ((B.statusPoint == "FullZaxvatRed" && A.statusPoint == "FullZaxvatRed") || (B.statusPoint == "FullZaxvatBlue" && A.statusPoint == "FullZaxvatBlue") || (B.statusPoint == "FullZaxvatBlue" && A.statusPoint == "FullZaxvatRed") || (B.statusPoint == "FullZaxvatRed" && A.statusPoint == "FullZaxvatBlue"))
                {
                    if(Vector3.Distance(transform.position, pointC.position) < PointDistanse)
                    {
                        agent.isStopped = true;
                        animator.SetBool("idle", true);
                        animator.SetBool("run", false);
                        animator.SetBool("attack", false);
                    }
                    else if (C.statusPoint == "" || C.statusPoint == "ZaxvatBlue" || C.statusPoint == "ZaxvatRed")
                    {
                       agent.isStopped = false;
                       agent.SetDestination(pointC.position);
                       float distance = Vector3.Distance(transform.position, pointC.position);
                       animator.SetBool("idle", false);
                       animator.SetBool("run", true);
                       animator.SetBool("attack", false);
                    }
                }
            }
        }
        else
        {
            agent.isStopped = true;
            Cursor.SetActive(false);
            animator.SetBool("death", true);
            animator.SetBool("idle", false);
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
            healthBar.gameObject.SetActive(false);
            var coll = GetComponent<CapsuleCollider>();
            coll.center = new Vector3(0, 30, 0);
            Invoke("ColliderDead", 0.2f);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;    
        animator.SetTrigger("damage");
        Instantiate(Blood, bloodPoint.position, transform.rotation);
    }
    void ColliderWeapon()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }
    void ColliderDead()
    {
        Destroy(gameObject);
    }
        
}

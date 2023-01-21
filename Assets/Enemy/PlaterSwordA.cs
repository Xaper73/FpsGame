using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using UnityEngine.Serialization;

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
    public GameObject[] enemy;

    private void Start()
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        weapon.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Player");

        targets = new Transform[enemy.Length];

        for (int i = 0; i < enemy.Length; i++)
        {
            targets[i] = enemy[i].GetComponent<Transform>();


            healthBar.value = HP;

            if (HP > 0)
            {
                if (Vector3.Distance(transform.position, enemy[i].transform.position) < AgroDistance)
                {
                    transform.LookAt(enemy[i].transform.position);
                    agent.SetDestination(enemy[i].transform.position);
                    float distance = Vector3.Distance(transform.position, enemy[i].transform.position);
                    agent.isStopped = false;
                    ResetAnimation("run");
                }

                if ((Vector3.Distance(transform.position, enemy[i].transform.position) < AgroDistance) &&
                    (Vector3.Distance(transform.position, enemy[i].transform.position) < AttackDistance))
                {
                    if (timeBtwShors <= 0)
                    {
                        ResetAnimation("attack");
                        agent.isStopped = true;
                        transform.LookAt(enemy[i].transform.position);
                        weapon.GetComponent<Collider>().enabled = true;
                        Invoke("ColliderWeapon", 0.6f);
                        timeBtwShors = startTimeBtwShors;
                    }
                    else
                    {
                        agent.isStopped = true;
                        transform.LookAt(enemy[i].transform.position);
                        ResetAnimation("idle");
                        timeBtwShors -= Time.deltaTime;
                    }
                }

                if (Vector3.Distance(transform.position, enemy[i].transform.position) > AgroDistance)
                {
                    if (A.statusPoint == "" || A.statusPoint == "ZaxvatBlue" || A.statusPoint == "ZaxvatRed")
                    {
                        if (Vector3.Distance(transform.position, pointA.position) < PointDistanse)
                        {
                            agent.isStopped = true;
                            ResetAnimation("idle");
                        }
                        else if (A.statusPoint != "FullZaxvatRed" || A.statusPoint != "FullZaxvatBlue")
                        {
                            agent.SetDestination(pointA.position);
                            float distance = Vector3.Distance(transform.position, pointA.position);
                            ResetAnimation("run");
                        }
                    }

                    if (A.statusPoint == "FullZaxvatRed" || A.statusPoint == "FullZaxvatBlue")
                    {
                        if (Vector3.Distance(transform.position, pointB.position) < PointDistanse)
                        {
                            agent.isStopped = true;
                            ResetAnimation("idle");
                        }
                        else if (B.statusPoint == "" || B.statusPoint == "ZaxvatBlue" || B.statusPoint == "ZaxvatRed")
                        {
                            agent.isStopped = false;
                            agent.SetDestination(pointB.position);
                            float distance = Vector3.Distance(transform.position, pointB.position);
                            ResetAnimation("run");
                        }
                    }

                    if ((B.statusPoint == "FullZaxvatRed" && A.statusPoint == "FullZaxvatRed") ||
                        (B.statusPoint == "FullZaxvatBlue" && A.statusPoint == "FullZaxvatBlue") ||
                        (B.statusPoint == "FullZaxvatBlue" && A.statusPoint == "FullZaxvatRed") ||
                        (B.statusPoint == "FullZaxvatRed" && A.statusPoint == "FullZaxvatBlue"))
                    {
                        if (Vector3.Distance(transform.position, pointC.position) < PointDistanse)
                        {
                            agent.isStopped = true;
                            ResetAnimation("idle");
                        }
                        else if (C.statusPoint == "" || C.statusPoint == "ZaxvatBlue" || C.statusPoint == "ZaxvatRed")
                        {
                            agent.isStopped = false;
                            agent.SetDestination(pointC.position);
                            float distance = Vector3.Distance(transform.position, pointC.position);
                            ResetAnimation("run");
                        }
                    }
                }
            }
            else
            {
                agent.isStopped = true;
                Cursor.SetActive(false);
                ResetAnimation("death");
                healthBar.gameObject.SetActive(false);
                var coll = GetComponent<CapsuleCollider>();
                coll.center = new Vector3(0, 30, 0);
                Invoke("ColliderDead", 0.2f);
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    public void ResetAnimation(string nameStateAnimation)
    {
        animator.SetBool("death", false);
        animator.SetBool("idle", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", false);

        animator.SetBool(nameStateAnimation, true);
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
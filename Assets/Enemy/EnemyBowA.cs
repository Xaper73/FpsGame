using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBowA : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
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
    public Transform BulletEnemy;
    public Transform PointBullet;
    public GameObject Cursor;
    public GameObject Blood;
    public Transform bloodPoint;
    public float timeBtwShors;
    public float startTimeBtwShors;

    private void Start()
    {
        agent = animator.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar.value = HP;
        if (HP > 0)
        {
            if (Vector3.Distance(transform.position, player.position) > AgroDistance)                                    // если дистанция до игрока больше
            { 
                if (A.statusPoint == "" || A.statusPoint == "ZaxvatBlue" || A.statusPoint == "ZaxvatRed")                // если статус точки А не захвачен или захватываеться синими или захватываеться красными
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
                    Instantiate(BulletEnemy, PointBullet.position, PointBullet.rotation);
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
//            EnemyObjectTeg.gameObject.SetActive(false);
//            EnemyObjectTeg.transform.position = EnemyObjectTeg.transform.position + new Vector3(0, 10, 0);
//            this.GetComponent<Collider>().size = new Vector3(0,0,0);
//            Destroy(this.GetComponent<Collider>());
            var coll = GetComponent<CapsuleCollider>();
            coll.center = new Vector3(0, 10, 0);
//            coll.height = 0;
//            coll.radius = 0;
            Invoke("ColliderDead", 0.5f);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;    
        animator.SetTrigger("damage");
        Instantiate(Blood, bloodPoint.position, transform.rotation);
    }
    void ColliderDead()
    {
        Destroy(this.GetComponent<Collider>());
        gameObject.tag = "Finish";
    }
}
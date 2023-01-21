using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerM : MonoBehaviour
{
    public float playerHP;
    public float HPMax;
//    public static bool isGameOver;
    public Image bar;
    public GameObject bloodOverlay;

    void Start()
    {
//        isGameOver = false;
        playerHP = HPMax;
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = playerHP / HPMax;

//        if (isGameOver)
//        {
//            SceneManager.LoadScene("EnemyDamage");
//        }
    }

    public void TakeDamage(int damageAmount)
    {
        playerHP -= damageAmount;
        bloodOverlay.SetActive(true);
        Invoke("Damage", 0.2f);    
//        animator.SetTrigger("damage");
//        Instantiate(Blood, bloodPoint.position, transform.rotation);
    }

    public void Damage()
    {
        bloodOverlay.SetActive(false);
    }
}

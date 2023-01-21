using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    public PointA A;
    public PointB B;
    public PointC C;
    public GameObject Win;
    public GameObject Loss;
    public Text timerText;
    public int Blue;
    public int Red;
    public float timeStart = 180;

    void Start()
    {
        timerText.text = timeStart.ToString().Replace(",", ":");
    }

    void Update()
    {
        timeStart -= Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString().Replace(",", ":");
        
        if (timeStart < 1)
        {
            Loss.SetActive(true);
            Time.timeScale = 0f;
        }

        if(A.Win == true && B.Win == true && C.Win == true)
        {
            Blue = A.BlueWinA + B.BlueWinB + C.BlueWinC;
            Red = A.RedWinA + B.RedWinB + C.RedWinC;
            
            if(Blue > Red)
            {
                Win.SetActive(true);
                Time.timeScale = 0f;
            }
            if(Red > Blue)
            {
                Loss.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
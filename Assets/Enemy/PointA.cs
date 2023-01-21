using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointA : MonoBehaviour
{
    private Coroutine CoroutineZaxvat;
    public bool teamBlue;
    public bool teamRed;
    public string statusPoint;
    public Image ImagePoint;
    public GameObject FlagBlue;
    public GameObject FlagRed;
    public Transform FlagB;
    public Transform FlagR;
    public float SpeedFlag;
    public float SpeedUI = 0.001f;
    public int BlueWinA = 0;
    public int RedWinA = 0;
    public bool Win = false;

    void Start()
    {
        FlagRed.SetActive(false);
        FlagBlue.SetActive(false);
        teamBlue = false;
        teamRed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CoroutineZaxvat == null) CoroutineZaxvat = StartCoroutine(Zaxvat());

        if (other.tag == "Player") teamBlue = true;
        if (other.tag == "Enemy") teamRed = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") teamBlue = true;
        if (other.tag == "Enemy") teamRed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") teamBlue = false;
        if (other.tag == "Enemy") teamRed = false;
    }

    IEnumerator Zaxvat()
    {
        while (true)
        {
            if(teamBlue == true && teamRed == false)      // если синие на точке
            {
                if(statusPoint == "" || statusPoint == "ZaxvatBlue")         // если статус точки не захвачен или захватываеться синими
                {
                    if(statusPoint != "ZaxvatBlue") statusPoint = "ZaxvatBlue";          // если статус точки не захватываеться синими то поменять статус на захватываеться синими
                    if(ImagePoint.color != new Color(0,0,1,1)) ImagePoint.color = new Color(0,0,1,1);         // если цвет UI не синий то поменять на синий
                    ImagePoint.fillAmount += SpeedUI;                            // меняем цвет картинки со скоростью

                    FlagBlue.SetActive(true);                                   // включить модель синего флага
                    FlagRed.SetActive(false);                                   // выключить модель красного флага
                    FlagB.transform.position = FlagB.transform.position + new Vector3(0, SpeedFlag, 0);           // подьем флага со скоростью вверх

                    if(ImagePoint.fillAmount == 1)                       // если UI полностью синяя
                    {
                    statusPoint = "FullZaxvatBlue";                      // то статус точки полностью захвачена
                    BlueWinA = 1;
                    Win = true;
                    }
                }
                else if(statusPoint == "ZaxvatRed")                      // если статус точки захватываеться красыми
                {
                    ImagePoint.fillAmount -= SpeedUI;                     // меняем цвет UI назад
                    FlagR.transform.position = FlagR.transform.position + new Vector3(0, -SpeedFlag, 0);    // опускаем красный флаг со скоростью вниз

                    if(ImagePoint.fillAmount == 0) statusPoint = "";     // если картинка серая то статус точки не захватываеться
                }
            }
            else if(teamRed == true && teamBlue == false)
            {
                if(statusPoint == ""  || statusPoint == "ZaxvatRed")
                {
                    if(statusPoint != "ZaxvatRed") statusPoint = "ZaxvatRed";
                    if(ImagePoint.color != new Color(1,0,0,1)) ImagePoint.color = new Color(1,0,0,1);
                    ImagePoint.fillAmount += SpeedUI;

                    FlagRed.SetActive(true);
                    FlagBlue.SetActive(false);
                    FlagR.transform.position = FlagR.transform.position + new Vector3(0, SpeedFlag, 0);             

                    if(ImagePoint.fillAmount == 1) 
                    {
                    statusPoint = "FullZaxvatRed";
                    RedWinA = 1;
                    Win = true;
                    }
                }
                else if(statusPoint == "ZaxvatBlue")
                {
                    ImagePoint.fillAmount -= SpeedUI;
                    FlagB.transform.position = FlagB.transform.position + new Vector3(0, -SpeedFlag, 0); 

                    if(ImagePoint.fillAmount == 0) statusPoint = "";
                }
            }
            else if(teamBlue == false && teamRed == false)                // если на точке нет синих или красных
            {
                if(statusPoint == "ZaxvatRed")                // если статус точки захватываеться красными
                {
                    if(ImagePoint.fillAmount != 0)               // если UI не серая
                    {
                        ImagePoint.fillAmount -= SpeedUI;          // меняем цвет UI назад
                        FlagR.transform.position = FlagR.transform.position + new Vector3(0, -SpeedFlag, 0);     // опускаем красный флаг со скоростью вниз

                        if(ImagePoint.fillAmount == 0)
                        {
                            statusPoint = "";     // если картинка серая то статус точки не захватываеться
                            FlagRed.SetActive(false);
                        }
                    }
                }
                if(statusPoint == "ZaxvatBlue")
                {
                    if(ImagePoint.fillAmount != 0)
                    {
                        ImagePoint.fillAmount -= SpeedUI;
                        FlagB.transform.position = FlagB.transform.position + new Vector3(0, -SpeedFlag, 0);

                        if(ImagePoint.fillAmount == 0)
                        {  
                            statusPoint = "";
                            FlagBlue.SetActive(false);
                        } 
                    }
                }
            }
            else if(teamBlue == true && teamRed == true)                // если на точке синие и красные
            {

            }
            yield return new WaitForFixedUpdate();
        }
    }
}

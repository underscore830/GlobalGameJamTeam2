using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DarkController : MonoBehaviour
{
    public PlayerController playerController;
    public Image image;
    public float darkSpeed = 0.01f,countTimeDark = 30, lightSpeed = 0.1f, countTimeLight = 10;
    private bool isWater;
    bool inCoroutine, inLighting = false, inDarking =false;
    public GameObject lightMeterUI;
    private bool playerLighting = true;
    private void Start()
    {
        isWater = false;

    }
    private void Update()
    {
        if (isWater)
        {
            if (!inLighting)
            {
                if (inCoroutine)
                {
                    StopAllCoroutines();
                    inCoroutine = false;
                }
                StartCoroutine(ChangeDisplay(countTimeLight, -lightSpeed, 0));
                inCoroutine = true;
                inLighting = true;
                inDarking = false;
            }
        }
        else
        {
            if (!inDarking)
            {
                if (inCoroutine)
                {
                    StopAllCoroutines();
                    inCoroutine = false;
                }
                StartCoroutine(ChangeDisplay(countTimeDark, darkSpeed, 1));
                inCoroutine = true;
                inDarking = true;
                inLighting = false;
            }
        }
        playerLighting = image.color.a <= 1;

        //Debug.Log(inCoroutine+"coroutine"+inDarking+"darking"+inLighting+"lighting");
    }

    IEnumerator ChangeDisplay(float time,float speed,float color_a)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed);
        if (isWater)
        {
            while (image.color.a >= color_a)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed);
                //Debug.Log(image.color);
                lightMeterUI.GetComponent<LightMeter>().updateLightMeter(1 - image.color.a);
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            while (image.color.a <= color_a)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed);
                //Debug.Log(image.color);
                lightMeterUI.GetComponent<LightMeter>().updateLightMeter(1 - image.color.a);
                yield return new WaitForSeconds(time);
            }
        }
    }

    public void SetIsWater(bool water)
    {
        isWater = water;
    }

    public bool GetPlayerLighting()
    {
        return playerLighting;
    }

}

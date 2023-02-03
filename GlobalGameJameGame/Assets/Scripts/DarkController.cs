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
    }

    IEnumerator ChangeDisplay(float time,float speed,float color_a)
    {
        while (image.color.a != color_a)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + speed);
            yield return new WaitForSeconds(time);
        }
    }

    public void SetIsWater(bool water)
    {
        isWater = water;
    }
}

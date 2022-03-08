using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Image border;

    public bool showAlways = false;
    float showTime = 0;



    private void Start()
    {
        //var tempColor = new Color(0.3f, 0.3f, 0.3f, fill.color.a);
        //fill.color = tempColor;
    }

    private void Update()
    {
        if (showAlways) showTime = 2;
        if (showTime >= 0)
        {
            if (showTime > 1)
            {
                SetAlpha(1);
            }
            else
            {
                SetAlpha(Mathf.Lerp(1, 0, 1 - showTime));
            }
            showTime -= Time.deltaTime;
        }
        else
        {
            SetAlpha(0);
            showTime = 0;
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void showBar(float durance)
    {
        showTime = durance;
    }

    public void SetAlpha(float alpha)
    {
        var tempColor = new Color(fill.color.r, fill.color.g, fill.color.b, alpha);
        fill.color = tempColor;
        border.color = tempColor;
    }
}

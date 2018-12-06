using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    private Image image;
    private PlayerFish player;

    private Color originalColor;

    private Coroutine dashFade;

    private float previousDashPercent;

    private void Awake()
    {
        image = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerFish>();
    }

    private void Start()
    {
        originalColor = image.color;
        previousDashPercent = player.dashReadyPercent;
        StartDashFade();
    }

    // Update is called once per frame
    void Update()
    {
        print(dashFade);
        if (player.dashReadyPercent >= 1 && previousDashPercent >= 1 && !hasRun)
        {
            print("Should start");
            StartDashFade();
        }
        else if (player.dashReadyPercent < 1)
        {
            if (hasRun == true)
            {
                hasRun = false;
            }
            if (dashFade != null)
            {
                StopDashFade();
            }
        }
        image.fillAmount = player.dashReadyPercent;
        previousDashPercent = player.dashReadyPercent;
    }

    private void StartDashFade()
    {
        dashFade = StartCoroutine(DashFade());
    }

    private void StopDashFade()
    {
        StopCoroutine(dashFade);
        image.color = originalColor;
    }

    private bool hasRun = false;
    private float fadeTime = 0.3f;
    private IEnumerator DashFade()
    {
        float tick = 0;
        Color cl = originalColor;
        while (tick < fadeTime)
        {
            tick += Time.deltaTime;
            cl.a = 1 - tick / fadeTime;
            image.color = cl;
            yield return null;
        }
        hasRun = true;
        dashFade = null;
        3}
}

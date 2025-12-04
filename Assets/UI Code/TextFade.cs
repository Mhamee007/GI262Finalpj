using UnityEngine;
using UnityEngine.Events;

public class TextFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public UnityEvent OnFadeOutComplete;

    private float timer = 0f;
    private bool fadingIn = false;
    private bool fadingOut = false;

    void Awake()
    {
        canvasGroup.alpha = 0;
    }

    public void StartFadeIn()
    {
        timer = 0;
        fadingIn = true;
        fadingOut = false;
    }

    public void StartFadeOut()
    {
        timer = 0;
        fadingIn = false;
        fadingOut = true;
    }

    void Update()
    {
        if (fadingIn)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);

            if (canvasGroup.alpha >= 1)
                fadingIn = false;
        }

        if (fadingOut)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(timer / fadeDuration);

            if (canvasGroup.alpha <= 0)
            {
                fadingOut = false;
                OnFadeOutComplete?.Invoke();   // แจ้งว่า fade ออกเสร็จแล้ว
            }
        }
    }
}

using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public TextFade[] creditTexts;
    public float showTime = 2f;

    private int index = 0;
    private float timer = 0;
    private bool showing = false;

    void Start()
    {
        foreach (var t in creditTexts)
        {
            t.gameObject.SetActive(false);
            t.OnFadeOutComplete.AddListener(ShowNext);
        }

        ShowNext();
    }

    void Update()
    {
        if (!showing) return;

        timer += Time.deltaTime;

        if (timer >= showTime)
        {
            showing = false;

            // เริ่มเฟดออก
            creditTexts[index - 1].StartFadeOut();
        }
    }

    void ShowNext()
    {
        // ปิดข้อความก่อนหน้า
        if (index - 1 >= 0)
        {
            creditTexts[index - 1].gameObject.SetActive(false);
        }

        // ถ้าโชว์ครบทุกข้อความแล้วให้จบ
        if (index >= creditTexts.Length)
            return;

        // เปิดข้อความใหม่
        var t = creditTexts[index];
        t.gameObject.SetActive(true);
        t.StartFadeIn();

        timer = 0;
        showing = true;

        index++;
    }
}

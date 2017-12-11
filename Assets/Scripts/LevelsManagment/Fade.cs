using UnityEngine;

public class Fade : MonoBehaviour
{

    public float FadeInTime;

    void Awake()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad < FadeInTime)
        {
            float alpha = Time.timeSinceLevelLoad / FadeInTime;
            GetComponent<CanvasGroup>().alpha = alpha;
        }
    }
}

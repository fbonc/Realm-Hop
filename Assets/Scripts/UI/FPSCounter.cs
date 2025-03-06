using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float updateInterval = 0.5f;

    // -----------------------------------------------------------------------------------------

    private float accum = 0f;
    private int frames = 0;
    private float timeLeft;

    void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPSCounter: No UI Text assigned.");
        }
        timeLeft = updateInterval;
    }

    void Update()
    {
        float fps = 1.0f / Time.deltaTime;
        accum += fps;
        frames++;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            float avgFps = accum / frames;
            fpsText.text = string.Format("FPS: {0:F1}", avgFps);

            timeLeft = updateInterval;
            accum = 0f;
            frames = 0;
        }
    }
}
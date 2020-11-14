using TMPro;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float _deltaTime;

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        
        SetFps();
    }

    private void SetFps()
    {
        var msec = _deltaTime * 1000.0f;
        
        var fps = 1.0f / _deltaTime;
        
        fpsText.SetText($"FPS: {fps:00.} ({msec:0.0} ms)");
    }
}
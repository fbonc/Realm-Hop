using UnityEngine;

public class QualityController : MonoBehaviour
{
    void Start() {
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = 60; // Set target frame rate to 60 fps (or another desired value)
    }
    public void SetQualityPreset(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        Debug.Log("Quality level set to: " + QualitySettings.names[qualityIndex]);
    }
}
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField]
    private int _targetFps;
    
    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = _targetFps;
    }
    

    [ContextMenu(nameof(PrintFPS))]
    public void PrintFPS()
    {
        Debug.Log(Application.targetFrameRate);
    }
}

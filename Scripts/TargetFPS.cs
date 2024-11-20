using UnityEngine;
using UniRx;

public class TargetFPS : MonoBehaviour
{
    public int FrameRate = 60;

    private void Awake()
    {
        UpdateFPS();
        this.ObserveEveryValueChanged(x => x.FrameRate)
            .Subscribe(_ => UpdateFPS());
    }

    private void UpdateFPS()
    {
#if UNITY_WEBGL
        // WebGLの場合vSyncCountを0、59fpsに設定
        QualitySettings.vSyncCount = 0;
        int frameRate = FrameRate;
        if (60 % FrameRate == 0)
        {
            --frameRate; // 割り切れる場合-1
        }
        Application.targetFrameRate = frameRate;
#else
        Application.targetFrameRate = FrameRate;
#endif
    }
}
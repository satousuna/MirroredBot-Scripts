using UnityEngine;

[CreateAssetMenu(menuName = "PlayerRunData")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerRunData : ScriptableObject
{
	[Header("Run")]
	public float runMaxSpeed; //目標速度
	public float runAcceleration; //目標速度到達にかかる時間
	[HideInInspector] public float runAccelAmount; //加速時プレイヤーに加える力
	public float runDecceleration; //目標速度から停止にかかる時間
	[HideInInspector] public float runDeccelAmount; //減速時プレイヤーに加える力
	[Space(10)]
	[Range(0.01f, 1)] public float accelInAir; //空中での加速率に適用する乗数
	[Range(0.01f, 1)] public float deccelInAir;//空中での減速率に適用する乗数
	public bool doConserveMomentum;


    private void OnValidate()
    {
		//(1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeedで加減速の力を計算
		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		#region Variable Ranges
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		#endregion
	}
}
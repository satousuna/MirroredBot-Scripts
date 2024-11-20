using UnityEngine;
using System.Collections;
 
public class ParticleTrailChase : MonoBehaviour { 
    [SerializeField] private GameObject Player;
 
    private void OnEnable() 
    {
        Vector2 PlayerPosition = Player.transform.position;//初期位置にプレイヤーの位置を代入
        transform.position = PlayerPosition;//初期位置を反映
    }
    
    
}

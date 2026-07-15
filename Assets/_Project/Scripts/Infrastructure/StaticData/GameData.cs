using UnityEngine;

[CreateAssetMenu(menuName = "StaticData/Game", fileName = "GameStaticData")]
public class GameData : ScriptableObject
{
    public float AnimationItemsSpeed = 2f;
    public float AnimationItemsAmplitude = 5f;
    public int SpawnIDCoefficient;
    public float SaveTimer = 0.5f;
    public float AttackTimer = 1f;
    public float MultiplierTimer = 5f;
    public int MultiplierValue = 5;
}
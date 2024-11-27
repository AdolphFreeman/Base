using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Attack Mode")]
public class EnemyAttackMode : ScriptableObject
{
    public EnemyAttackType type;
    //public 
    public GameObject bulletPrefab;
    public int bulletNum;
    public float spacing;
    public float velocity;
    public float rate;
}

public enum EnemyAttackType
{
    Straight, Radial, Spiral, Spread
}

public enum EnemyLaserType
{
    Floating, Hold, 
}
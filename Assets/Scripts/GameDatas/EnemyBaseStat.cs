using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBaseStat", menuName = "Scriptable Objects/EnemyBaseStat")]
public class EnemyBaseStat : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _baseDef;
    [SerializeField] private int _baseSpeed;
    [SerializeField] private int _movementRange;
    [SerializeField] private int _maxMP;

    public string EnemyName => _enemyName;
    public int MaxHealth => _maxHealth;
    public int BaseDamage => _baseDamage;
    public int BaseDef => _baseDef;
    public int BaseSpeed => _baseSpeed;
    public int MovementRange => _movementRange;
    public int MaxMP => _maxMP;

}

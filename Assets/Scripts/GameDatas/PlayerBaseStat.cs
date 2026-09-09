using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStat", menuName = "Scriptable Objects/PlayerBaseStat")]
public class PlayerBaseStat : ScriptableObject
{
    [SerializeField] private string _pcName;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _baseDef;
    [SerializeField] private int _baseSpeed;
    [SerializeField] private int _baseAttackRange;
    [SerializeField] private int _movementRange;
    [SerializeField] private int _maxMP;

    public string PCName => _pcName;
    public int MaxHealth => _maxHealth;
    public int BaseDamage => _baseDamage;
    public int BaseDef => _baseDef;
    public int BaseSpeed => _baseSpeed;
    public int BaseAttackRange => _baseAttackRange;
    public int MovementRange => _movementRange;
    public int MaxMP => _maxMP;
}

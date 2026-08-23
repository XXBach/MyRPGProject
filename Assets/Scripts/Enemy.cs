using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int CurrentHealth;
    public int CurrentAttack;
    public int CurrentDefense;
    public int CurrentMovementRange;
    public int CurrentSpeed;
}
public class Enemy : MonoBehaviour
{
    private EnemyData _currentDatas;
    [SerializeField] private EnemyBaseStat _baseStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _currentDatas = new EnemyData();
        _currentDatas.CurrentHealth = _baseStats.MaxHealth;
        _currentDatas.CurrentAttack = _baseStats.BaseDamage;
        _currentDatas.CurrentDefense = _baseStats.BaseDef;
        _currentDatas.CurrentMovementRange = _baseStats.MovementRange;
        _currentDatas.CurrentSpeed = _baseStats.BaseSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

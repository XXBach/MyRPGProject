using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string Name;
    public int CurrentHealth;
    public int CurrentAttack;
    public int CurrentDefense;
    public int CurrentMovementRange;
    public int CurrentSpeed;
    public int CurrentMP;
}
public class Enemy : MonoBehaviour, ICharacter
{
    public EnemyData CurrentDatas { get; set; }
    [SerializeField] private EnemyBaseStat _baseStats;
    private EnemyMovement _enemyMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        CurrentDatas = new EnemyData();
        CurrentDatas.CurrentHealth = _baseStats.MaxHealth;
        CurrentDatas.CurrentAttack = _baseStats.BaseDamage;
        CurrentDatas.CurrentDefense = _baseStats.BaseDef;
        CurrentDatas.CurrentMovementRange = _baseStats.MovementRange;
        CurrentDatas.CurrentSpeed = _baseStats.BaseSpeed;
        CurrentDatas.CurrentMP = _baseStats.MaxMP;
        CurrentDatas.Name = _baseStats.EnemyName;
    }

    // Update is called once per frame
    private void Update()
    {
    }
    public Vector3 GetCharWorldPosition()
    {
        return transform.position;
    }
    public IMovement GetMovementManager()
    {
        return _enemyMovement;
    }
    public int GetCurrentSpeed()
    {
        return CurrentDatas.CurrentSpeed;
    }
    public int GetCurrentMP()
    {
        return CurrentDatas.CurrentMP;
    }
    public string GetName()
    {
        return CurrentDatas.Name;
    }
}

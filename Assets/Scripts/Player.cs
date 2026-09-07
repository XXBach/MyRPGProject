using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentHealth;
    public int CurrentAttack;
    public int CurrentDefense;
    public int CurrentMovementRange;
    public int CurrentSpeed;
    public int CurrentMP;
    public string Name;
}
public class Player : MonoBehaviour, ICharacter
{
    public PlayerData CurrentDatas { get; set; }
    [SerializeField] private PlayerBaseStat _baseStats;
    [SerializeField] private PlayerMovement _movementManager;
    private void Awake()
    {
        _movementManager ??= GetComponent<PlayerMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        CurrentDatas = new PlayerData();
        CurrentDatas.CurrentHealth = _baseStats.MaxHealth;
        CurrentDatas.CurrentAttack = _baseStats.BaseDamage;
        CurrentDatas.CurrentDefense = _baseStats.BaseDef;
        CurrentDatas.CurrentMovementRange = _baseStats.MovementRange;
        CurrentDatas.CurrentSpeed = _baseStats.BaseSpeed;
        CurrentDatas.CurrentMP = _baseStats.MaxMP;
        CurrentDatas.Name = _baseStats.PCName;
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
        return _movementManager;
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

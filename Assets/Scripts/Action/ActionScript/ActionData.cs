using UnityEngine;
public enum ActionType
{
    NormalAttack = 0,
    SpecialAttack = 1,
    Skill = 2,
}

[CreateAssetMenu(fileName = "ActionData", menuName = "Scriptable Objects/ActionData")]
public class ActionData : ScriptableObject
{
    [SerializeField] private ActionType _actionType;
    [SerializeField] private int _actionRange;
    [SerializeField] private int _skillMultiplier;
    [SerializeField] private int _manaCost;
    [SerializeField] private int _cooldownTime;

    public ActionType ActionType {  get { return _actionType; } set { _actionType = value; } }
    public int ActionRange {  get { return _actionRange; } set { _actionRange = value; } }
    public int SkillMultiplier {  get { return _skillMultiplier; } set { _skillMultiplier = value; } }
    public int ManaCost {  get { return _manaCost; } set { _manaCost = value; } }
    public int CooldownTime {  get { return _cooldownTime; } set { _cooldownTime = value; } }
}

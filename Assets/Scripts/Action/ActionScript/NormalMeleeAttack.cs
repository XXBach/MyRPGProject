using UnityEngine;

public class NormalMeleeAttack : IAction
{
    private ActionData _actionData;
    private int _damage;
    public NormalMeleeAttack(ActionData actionData)
    {
        _actionData = actionData;
    }

    public void Execute(ICharacter attacker, ICharacter target)
    {
        // Calculate damage based on attacker's stats and action data
        _damage = _actionData.SkillMultiplier * (attacker.CurrentDatas.CurrentAttack - target.CurrentDatas.CurrentDefense);
        // Apply damage to the target
        target.CurrentDatas.CurrentHealth -= _damage;
        PlayAttackAnim();
    }
    public void PlayAttackAnim()
    {
        // Play attack animation
        Debug.Log("Playing normal melee attack animation");
    }
}

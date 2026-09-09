using UnityEngine;

public class NormalMeleeAttack : IAction
{
    private ActionData _actionData;
    private int _damageDealt;
    public NormalMeleeAttack(ActionData actionData)
    {
        _actionData = actionData;
        _damageDealt = actionData.DamageDealt;
    }

    public void Execute(ICharacter attacker, ICharacter target)
    {
        // Calculate damage based on attacker's stats and action data
        int damage = _damageDealt + attacker.CurrentDatas.CurrentAttack - target.CurrentDatas.CurrentDefense;
        damage = Mathf.Max(damage, 0); // Ensure damage is not negative
        // Apply damage to the target
        target.CurrentDatas.CurrentHealth -= damage;
        // Optionally, you can add additional effects or animations here
        Debug.Log($"{attacker.GetName()} attacked {target.GetName()} for {damage} damage!");
    }
}

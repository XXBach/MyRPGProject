using UnityEngine;
public interface ICharacter
{
    public Vector3 GetCharWorldPosition();
    public IMovement GetMovementManager();
    public int GetCurrentSpeed();
    public int GetCurrentMP();
    public string GetName();
}

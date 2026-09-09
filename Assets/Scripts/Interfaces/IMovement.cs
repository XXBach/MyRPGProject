using UnityEngine;
using UnityEngine.Events;
public interface IMovement
{
    public UnityEvent<OnMovementEndArgs> GetOnMovementEnd();
    public void SetMovementState(MovementState state);
    
}

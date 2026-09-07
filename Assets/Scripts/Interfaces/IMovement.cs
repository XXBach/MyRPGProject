using UnityEngine;
using UnityEngine.Events;
public interface IMovement
{
    public UnityEvent GetOnMovementEnd();
    public void SetMovementState(MovementState state);
    
}

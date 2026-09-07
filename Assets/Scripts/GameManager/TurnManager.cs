using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;
using TMPro;
/*<Summary>
    TurnManager quản lý thứ tự lượt của các unit trong game.
    TurnManager sẽ gọi PlayerActionMenu.ShowMenuFor(unit) mỗi khi tới lượt 1 unit
    PlayerActionMenu sẽ lo việc còn lại
    Khi Player chọn hết lượt, PlayerActionMenu sẽ gọi TurnManager.EndTurn() để chuyển lượt sang unit tiếp theo
    Khi toàn bộ List<ICharacter> đã hết lượt, TurnManager sẽ gọi StartNewRound() để reset lượt cho tất cả unit
</Summary>*/
public enum TurnState
{
    PLAYER_TURN = 0,
    ENEMY_TURN = 1,
}
public enum TurnManagerPhase
{
    STARTTURN = 0,
    EXECUTETURN = 1,
    WAITINGFORENDTURNSIGNAL = 2,
    ENDTURN = 3,
}
//[Serializable] 
//public class ICharacter
//{
//    [SerializeField] private MonoBehaviour _characters;
//    public ICharacter Character => _characters as ICharacter;
//}
public class TurnManager : MonoBehaviour
{
    private int _turnNumber;
    [SerializeField] private PlayerActionMenu _actionMenu;
    private List<ICharacter> _characterList = new List<ICharacter>();
    private TurnManagerPhase _currentPhase;
    private UnityEvent _endTurnEvent;
    private int currentCharacterIndex = 0;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentPhase = TurnManagerPhase.STARTTURN;
        _turnNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentPhase)
        {
            case TurnManagerPhase.STARTTURN:
                HandleStartTurn();
                break;
            case TurnManagerPhase.EXECUTETURN:
                HandleTurnExecution(currentCharacterIndex);
                break;
            case TurnManagerPhase.WAITINGFORENDTURNSIGNAL:
                HandleTurnExecution(currentCharacterIndex);
                break;
            case TurnManagerPhase.ENDTURN:
                HandleEndTurn();
                break;
        }
    }
    //Khi state đang ở startturn, hàm này sẽ sắp xếp danh sách các unit theo thứ tự speed có sẵn, nếu bằng thì gọi đến thanh MP, nếu bằng nữa thì random, và gọi ShowMenuFor(unit) cho unit đầu tiên trong danh sách
    public void HandleStartTurn()
    {
        List<ICharacter> OrderedTurns = OrderingList();
        this._actionMenu.ShowMenuFor(OrderedTurns[0]);
        Debug.Log("Menu Shown");
        this._turnNumber++;
        SetTurnNumber();
        _currentPhase = TurnManagerPhase.WAITINGFORENDTURNSIGNAL;
    }
    public void HandleTurnExecution(int i)
    {
        List<ICharacter> OrderedTurns = OrderingList();
        this._actionMenu.ShowMenuFor(OrderedTurns[i]);
        _currentPhase = TurnManagerPhase.WAITINGFORENDTURNSIGNAL;
    }
    public void HandleEndTurnSignal()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex >= _characterList.Count) _currentPhase = TurnManagerPhase.ENDTURN;
        else _currentPhase = TurnManagerPhase.EXECUTETURN;
    }
    public void HandleEndTurn()
    {
        _endTurnEvent?.Invoke();
        _currentPhase = TurnManagerPhase.STARTTURN;
    }
    public List<ICharacter> OrderingList()
    {
        List<ICharacter> orderedList = new List<ICharacter>();
        ICharacter characterRef = null;
        for (int i = 0; i < _characterList.Count; i++) {
            characterRef = GetHighestSpeedCharRef();
            orderedList.Add(characterRef);
            _characterList.Remove(characterRef);
        }
        _characterList = orderedList;
        return orderedList;
    }
    public ICharacter GetHighestSpeedCharRef()
    {
        ICharacter CurrentCharRef = _characterList[0];
        foreach (var characterRef in _characterList)
        {
            if (CurrentCharRef.GetCurrentSpeed() > characterRef.GetCurrentSpeed()) CurrentCharRef = characterRef;
            else if (CurrentCharRef.GetCurrentSpeed() == characterRef.GetCurrentSpeed()) return GetHigherMPCharRef(CurrentCharRef,characterRef);
            else continue;
        }
        return CurrentCharRef;
    }
    public ICharacter GetHigherMPCharRef(ICharacter characterA, ICharacter characterB)
    {
        if (characterA.GetCurrentMP() > characterB.GetCurrentMP()) return characterA;
        else if (characterA.GetCurrentMP() < characterB.GetCurrentMP()) return characterB;
        else
        {
            bool isCharA = Random.value < 0.5f;             
            return isCharA ? characterA : characterB;
        }
    }
    public void RegisterCharacter(ICharacter character)
    {
        if (!_characterList.Contains(character))
        {
            _characterList.Add(character);
        }
    }
    public void SetTurnNumber()
    {
        _textMeshPro.text = "Turn: " + _turnNumber;
    }
}

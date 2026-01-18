using UnityEngine;

[CreateAssetMenu(fileName = "MoneyChangeEventSo", menuName = "Scriptable Objects/MoneyChangeEventSo")]
public class MoneyChangeEvent : FieldEventSO
{
    [SerializeField] private int _amount;
    public override void Activate(PlayerController playerController)
    {
        base.Activate(playerController);
        playerController.Money += _amount;
    }
}

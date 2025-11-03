using UnityEngine;

[CreateAssetMenu(fileName = "MoneySetterEventSo", menuName = "Scriptable Objects/MoneySetterEventSo")]
public class MoneySetterEventSo : FieldEventSO
{
    [SerializeField] private int _amount;
    public override void Activate(PlayerController playerController)
    {
        playerController.Money = _amount;
    }
}

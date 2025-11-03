using UnityEngine;

[CreateAssetMenu(fileName = "PricesIncreaseEventSo", menuName = "Scriptable Objects/PricesIncreaseEventSo")]
public class PricesIncreaseEventSo : FieldEventSO
{
    [SerializeField] private float _amount;
    public override void Activate(PlayerController playerController)
    {
        playerController.IncreasePrices(_amount);
    }
}

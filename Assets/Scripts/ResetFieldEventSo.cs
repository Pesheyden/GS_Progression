using UnityEngine;

[CreateAssetMenu(fileName = "ResetFieldEventSo", menuName = "Scriptable Objects/ResetFieldEventSo")]
public class ResetFieldEventSo : FieldEventSO
{
    public override void Activate(PlayerController playerController)
    {
        playerController.Reset();
    }
}

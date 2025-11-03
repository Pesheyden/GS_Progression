using System;
using UnityEngine;

public enum PropertyType
{
    Stores,
    Factories,
    Hotels,
}

[CreateAssetMenu(fileName = "PropertyChangerEnventSo", menuName = "Scriptable Objects/PropertyChangerEnventSo")]
public class PropertyChangerEnventSo : FieldEventSO
{
    [SerializeField] private PropertyType _propertyType;
    [SerializeField] private int _changeAmount;
    
    public override void Activate(PlayerController playerController)
    {
        switch (_propertyType)
        {
            case PropertyType.Stores:
                playerController.Stores += _changeAmount;
                break;
            case PropertyType.Factories:
                playerController.Factories += _changeAmount;
                break;
            case PropertyType.Hotels:
                playerController.Hotels += _changeAmount;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

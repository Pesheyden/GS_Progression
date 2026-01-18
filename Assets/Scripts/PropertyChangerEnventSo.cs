using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private bool _randomProperty;
    
    public override void Activate(PlayerController playerController)
    {
        base.Activate(playerController);
        if (_randomProperty)
        {
            switch (Random.Range(0,3))
            {
                case 0:
                    playerController.Stores += _changeAmount;
                    break;
                case 1:
                    playerController.Factories += _changeAmount;
                    break;
                case 2:
                    playerController.Hotels += _changeAmount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return;
        }

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

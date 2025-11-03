using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    public PlayerController OtherPlayer;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _turnText;
    [SerializeField] private TextMeshProUGUI _dicesText;
    [SerializeField] private TextMeshProUGUI _storesText;
    [SerializeField] private TextMeshProUGUI _factoriesText;
    [SerializeField] private TextMeshProUGUI _hotelsText;
    [SerializeField] private TextMeshProUGUI _bribeText;
    [SerializeField] private TextMeshProUGUI _politicalPartyText;

    [Header("Values")]
    [SerializeField] private int _money;
    [SerializeField] private int _dices = 1;
    [SerializeField] private int _stores;
    [SerializeField] private int _factories;
    [SerializeField] private int _hotels;
    [SerializeField] private bool _bribe;
    [SerializeField] private bool _politicalParty;

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            _moneyText.text = _money.ToString();
        }
    }
    public int  Dices
    {
        get => _dices;
        set
        {
            _dices = value;
            _dicesText.text = _dices.ToString();
        }
    }
    public int  Stores
    {
        get => _stores;
        set
        {
            _stores = value;
            _storesText.text = _stores.ToString();
        }
    }
    public int  Factories
    {
        get => _factories;
        set
        {
            _factories = value;
            _factoriesText.text = _factories.ToString();
        }
    }
    public int  Hotels
    {
        get => _hotels;
        set
        {
            _hotels = value;
            _hotelsText.text = _hotels.ToString();
        }
    }
    public bool Bribe
    {
        get => _bribe;
        set
        {
            _bribe = value;
            _bribeText.text = _bribe.ToString();
        }
    }
    public bool PoliticalParty
    {
        get => _politicalParty;
        set
        {
            _politicalParty = value;
            _politicalPartyText.text = _politicalParty.ToString();
        }
    }
    
    [Header("Prices")]
    [SerializeField] private int _baseDicePrice;
    [SerializeField] private int _baseStorePrice;
    [SerializeField] private int _baseFactoryPrice;
    [SerializeField] private int _baseHotelPrice;
    [SerializeField] private int _baseBribePrice;
    [SerializeField] private int _basePoliticalPartyPrice;
    [SerializeField] private int _baseResetPrice;
    
    [SerializeField] private TextMeshProUGUI _dicePriceText;
    [SerializeField] private TextMeshProUGUI _storePriceText;
    [SerializeField] private TextMeshProUGUI _factoryPriceText;
    [SerializeField] private TextMeshProUGUI _hotelPriceText;
    [SerializeField] private TextMeshProUGUI _bribePriceText;
    [SerializeField] private TextMeshProUGUI _politicalPartyPriceText;
    [SerializeField] private TextMeshProUGUI _resetPriceText;
    
    private int _dicePrice;
    private int _storePrice;
    private int _factoryPrice;
    private int _hotelPrice;
    private int _bribePrice;
    private int _politicalPartyPrice;
    private int _resetPrice;

    public int DicePrice
    {
        get => _dicePrice;
        set
        {
            _dicePrice = value;
            _dicePriceText.text = (_dicePrice * _priceMultiplier).ToString();
        }
    }
    public int StorePrice
    {
        get => _storePrice;
        set
        {
            _storePrice = value;
            _storePriceText.text = (_storePrice * _priceMultiplier).ToString();
        }
    }
    public int FactoryPrice
    {
        get => _factoryPrice;
        set
        {
            _factoryPrice = value;
            _factoryPriceText.text = (_factoryPrice * _priceMultiplier).ToString();
        }
    }
    public int HotelPrice
    {
        get => _hotelPrice;
        set
        {
            _hotelPrice = value;
            _hotelPriceText.text = (_hotelPrice * _priceMultiplier).ToString();
        }
    }
    public int BribePrice
    {
        get => _bribePrice;
        set
        {
            _bribePrice = value;
            _bribePriceText.text = (_bribePrice * _priceMultiplier).ToString();
        }
    }
    public int PoliticalPartyPrice
    {
        get => _politicalPartyPrice;
        set
        {
            _politicalPartyPrice = value;
            _politicalPartyPriceText.text = (_politicalPartyPrice * _priceMultiplier).ToString();
        }
    }
    public int ResetPrice
    {
        get => _resetPrice;
        set
        {
            _resetPrice = value;
            _resetPriceText.text = (_resetPrice * _priceMultiplier).ToString();
        }
    }

    [Header("Income")] 
    [SerializeField] private int _storeIncome;
    [SerializeField] private int _factoryIncome;
    [SerializeField] private int _hotelIncome;
    [SerializeField] private float _bribeIncomeRaise;

    private int _turn;
    private int _lastRoll;
    private float _priceMultiplier = 1;
    private float _incomeMultiplier = 1;
    private bool _resetUsed = false;

    [SerializedDictionary("Field", "Event")] [SerializeField]
    private SerializedDictionary<int,List<FieldEventSO>> _fieldEvents;

    private void Awake()
    {
        DicePrice = _baseDicePrice;          
        StorePrice = _baseStorePrice;         
        FactoryPrice = _baseFactoryPrice;       
        HotelPrice = _baseHotelPrice;         
        BribePrice = _baseBribePrice;         
        PoliticalPartyPrice = _basePoliticalPartyPrice;
        ResetPrice = _baseResetPrice;         
    }

    public void RollTheDice()
    {
        _turn++;
        _turnText.text = _turn.ToString();
        
        GlobalEvent();
        
        _lastRoll = (int)Mathf.Ceil(Random.Range(1 * _dices, 6 * _dices));
        GetIncome();
    }
    
    private void GlobalEvent()
    {
        /*if(!_politicalParty)
            _priceMultiplier += 0.25f;

        switch (_turn)
        {
            case 50:
                _money = 0;
                _moneyText.text = _money.ToString();
                break;
            case 75:
                Reset();
                break;
        }*/

        if (!_fieldEvents.TryGetValue(_turn, out var events)) return;
        
        foreach (var eventSo in events)
        {
            eventSo.Activate(this);
        }
    }

    public void IncreasePrices(float amount)
    {
        if (!_politicalParty)
            _priceMultiplier += amount;

        DicePrice = DicePrice;
        StorePrice = StorePrice;
        FactoryPrice = FactoryPrice;
        Hotels = Hotels;
        ResetPrice = ResetPrice;
        BribePrice = BribePrice;
        PoliticalPartyPrice = PoliticalPartyPrice;
    }
    private void GetIncome()
    {
        Money += _lastRoll;

        if (_lastRoll % 2 == 0)
        {
            Money += Mathf.RoundToInt(_stores * _storeIncome * _incomeMultiplier *
                                      (_lastRoll == 3 * _dices ? 1 : (_lastRoll < 3 * _dices ? 0.8f : 1.2f)));
            Money += Mathf.RoundToInt(_factories * _factoryIncome * _incomeMultiplier *
                                      (_lastRoll == 3 * _dices ? 1 : (_lastRoll < 3 * _dices ? 0.8f : 1.2f)));
        }
        else if (_lastRoll % 3 == 0)
        {
            Money += Mathf.RoundToInt(_hotels * _hotelIncome * _incomeMultiplier *
                                      (_lastRoll == 3 * _dices ? 1 : (_lastRoll < 3 * _dices ? 0.8f : 1.2f)));
        }
    }



    public void BuyStore()
    {
        if(!TryBuy(_storePrice))
            return;

        Stores++;

        StorePrice = _baseStorePrice * Stores;
    }

    public void BuyFactory()
    {
        if(!TryBuy(_factoryPrice))
            return;

        Factories++;

        FactoryPrice = _baseFactoryPrice * Factories;
    }

    public void BuyHotel()
    {
        if(!TryBuy(_hotelPrice))
            return;

        Hotels++;

        HotelPrice = _baseHotelPrice * Hotels;
    }

    public void TryBribe()
    {
        if(!TryBuy(_bribePrice) || Bribe)
            return;
        
        if(Random.Range(0f,1f) > 0.1f)
            return;

        _incomeMultiplier += _bribeIncomeRaise;
        Bribe = true;
        _bribeText.text = "V";
    }

    public void BuyPoliticalParty()
    {
        if(!TryBuy(_politicalPartyPrice) || PoliticalParty)
            return;

        _priceMultiplier = 1;
        PoliticalParty = true;
        _politicalPartyText.text = "V";
        
        DicePrice = DicePrice;
        StorePrice = StorePrice;
        FactoryPrice = FactoryPrice;
        Hotels = Hotels;
        ResetPrice = ResetPrice;
        BribePrice = BribePrice;
        PoliticalPartyPrice = PoliticalPartyPrice;
    }

    public void BuyReset()
    {
        if(!TryBuy(_resetPrice) || _resetUsed)
            return;

        _resetUsed = true;
        Reset();
        OtherPlayer.Reset();
    }

    public void BuyDice()
    {
        if(!TryBuy(_dicePrice))
            return;


        Dices++;

        DicePrice = Mathf.CeilToInt(_baseDicePrice * Mathf.Pow(2.2f, Dices));
    }

    private bool TryBuy(int price)
    {
        price = Mathf.CeilToInt(price * _priceMultiplier);
        if (Money - price < 0) return false;
        
        Money -= price;
        return true;

    }
    public void Reset()
    {
        Stores = 0;
        Factories = 0;
        Hotels = 0;
    }
}
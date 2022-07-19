using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _templaite;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private TextMeshProUGUI _playerCoins;

    private void Awake()
    {
        foreach (Weapon weapon in _weapons)
        {
            AddItem(weapon);
        }
    }

    private void OnEnable()
    {
        UpdatePlayerCoins();
    }

    private void OnDisable()
    {
        
    }

    private void AddItem(Weapon weapon)
    {
        WeaponView view = Instantiate(_templaite, _weaponContainer.transform);
        view.Init(weapon);
        view.BuyButtonClick += OnBuyButtonClick;
    }

    private void OnBuyButtonClick(Weapon weapon, WeaponView weaponView)
    {
        TrySellWeapon(weapon, weaponView);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView weaponView)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weaponView.BuyButtonClick -= OnBuyButtonClick;
            weapon.Buy();
        }
    }

    private void UpdatePlayerCoins()
    {
        _playerCoins.text = _player.Money.ToString();
    }
}

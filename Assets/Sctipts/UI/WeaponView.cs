using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Image _image;
    [SerializeField] private Button _buyButton;

    private Weapon _weapon;
     
    public UnityAction<Weapon, WeaponView> BuyButtonClick;

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
        _name.text = weapon.Label;
        _price.text = weapon.Price.ToString();
        _image.sprite = weapon.Icon;
    }

    private void TryLockBuyButton()
    {
        if (_weapon.IsBuyed)
        {
            _buyButton.interactable = false;
        }
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClick); 
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClick); 
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClick?.Invoke(_weapon, this);
        TryLockBuyButton();
    }
}

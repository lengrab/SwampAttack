using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    
    [SerializeField] protected Bullet Bullet;

    private bool _isBuyed = false;
    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;

    public abstract void Soot(Transform sootPoint);

    public void Buy()
    {
        _isBuyed = true;
    }
}

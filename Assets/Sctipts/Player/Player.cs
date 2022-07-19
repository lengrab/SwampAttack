using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour
{
    private static readonly int DiedHash = Animator.StringToHash("Died");
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _delayDied = 1;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private List<Weapon> _weapons;


    public Weapon _currentWeapon;
    private int _currentHealth;
    private Animator _animator;

    public event UnityAction<Player> PlayerDied;
    public event UnityAction<int, int> HealthChaged; 

    public int Health => _currentHealth;

    public int Money { get; private set; }

    public void Shoot()
    {
        if (_currentWeapon)
        {
            _currentWeapon.Soot(_shootPoint);
        }
    }

    public void ListOnEnemy(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= Mathf.Clamp(damage, 0, int.MaxValue);
        HealthChaged.Invoke(_currentHealth, _maxHealth);
        
        if (_maxHealth <= 0)
        {
            PlayerDied.Invoke(this);
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        _weapons.Add(weapon);
        Money -= weapon.Price;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        Money += Mathf.Clamp(enemy.Reward, 0, int.MaxValue);
    }

    private void OnPlayerDied(Player player)
    {
        StartCoroutine(Died());
    }

    private void Awake()
    {
        _weapons = new List<Weapon>();
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        PlayerDied = OnPlayerDied;
    }

    private IEnumerator Died()
    {
        _animator.Play(DiedHash);
        yield return new WaitForSeconds(_delayDied);
        Destroy(gameObject);
    }
}

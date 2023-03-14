
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class MainHudViewModel : MonoBehaviour, INotifyPropertyChanged
{ 
    private ActorsManager actorsManager;
    private Health playerhealth;
    private PlayerWeaponsManager weaponManager;

    private float health;
    private float ammo;
    private int enemiesLeft;
    private bool isShooting;
    private bool isPlaying; // For objective complete

    void Start()
    {
        GetComponent<NoesisView>().Content.DataContext = this;
        playerhealth = actorsManager.Player.GetComponent<Health>();
        EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);

        StartCoroutine(InitialEnemiesLeft());
        IsShooting = false;
        IsPlaying = false;
    }

    void Update()
    {
        // Set 'health' Rive input value
        Health = playerhealth.CurrentHealth;
        var activeThing = weaponManager.GetActiveWeapon();

        // Set 'shooting' Rive input value
        IsShooting = Input.GetMouseButtonDown(0);

        if (activeThing != null)
        {
            // Set 'ammo' Rive input value
            var ammoRatio = (int)Math.Round(activeThing.CurrentAmmoRatio * 100);
            Ammo = ammoRatio >= 0 ? ammoRatio : 0;
        }
    }

    IEnumerator InitialEnemiesLeft()
    {
        yield return new WaitForSeconds(1);
        EnemiesLeft = 2;
    }

    void OnEnemyKilled(EnemyKillEvent evt)
    {
        // Set 'Enemies Left' Rive input value
        EnemiesLeft = evt.RemainingEnemyCount;

        IsPlaying = EnemiesLeft <= 0;
    }

    private void Awake()
    {
        actorsManager = FindObjectOfType<ActorsManager>();
        weaponManager = FindObjectOfType<PlayerWeaponsManager>();
    }

    void OnDestroy()
    {
        EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public float Health
    {
        get => this.health;
        set
        {
            if (this.health == value) return;
            this.health = value;
            OnPropertyChanged();
        }
    }

    public float Ammo
    {
        get => this.ammo;
        set
        {
            if (this.ammo == value) return;
            this.ammo = value;
            OnPropertyChanged();
        }
    }

    public int EnemiesLeft
    {
        get => this.enemiesLeft;
        set
        {
            if (this.enemiesLeft == value) return;
            this.enemiesLeft = value;
            OnPropertyChanged();
        }
    }

    public bool IsShooting
    {
        get => this.isShooting;
        set
        {
            if (this.isShooting == value) return;
            this.isShooting = value;
            OnPropertyChanged();
        }
    }

    public bool IsPlaying
    {
        get => this.isPlaying;
        set
        {
            if (this.isPlaying == value) return;
            this.isPlaying = value;
            // Perhaps we can catch this event in XAML to then fire the Rive trigger?
            OnPropertyChanged();
        }
    }
}


using System;
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

    void Start()
    {
        GetComponent<NoesisView>().Content.DataContext = this;
        playerhealth = actorsManager.Player.GetComponent<Health>();
    }

    void Update()
    {
        Health = playerhealth.CurrentHealth;
        var weaponController = weaponManager.GetActiveWeapon();
        var ammoRatio = (int)Math.Round(weaponController.CurrentAmmoRatio * 100);

        if (ammoRatio >= 0)
        {
            Ammo = ammoRatio;
        }
        else
        {
            Ammo = 0;
        }
    }

    private void Awake()
    {
        actorsManager = FindObjectOfType<ActorsManager>();
        weaponManager = FindObjectOfType<PlayerWeaponsManager>();
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

}

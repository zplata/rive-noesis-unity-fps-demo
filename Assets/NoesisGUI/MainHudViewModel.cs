
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
    private PlayerInputHandler m_InputHandler;
    private WeaponController activeWeaponController;

    private float health;
    private float ammo;
    private int enemiesLeft;
    private bool isShooting;

    void Start()
    {
        GetComponent<NoesisView>().Content.DataContext = this;
        playerhealth = actorsManager.Player.GetComponent<Health>();
        EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
        m_InputHandler = GetComponent<PlayerInputHandler>();
        activeWeaponController = weaponManager.GetActiveWeapon();

        EnemiesLeft = 2;
    }

    void Update()
    {
        Health = playerhealth.CurrentHealth;
        WeaponController activeThing = weaponManager.GetActiveWeapon();
        if (activeThing != null && m_InputHandler != null)
        {
            /*            bool hasFired = activeThing.HandleShootInputs(
                            m_InputHandler.GetFireInputDown(),
                            m_InputHandler.GetFireInputHeld(),
                            m_InputHandler.GetFireInputReleased());*/
            bool hasFired = activeThing.CurrentAmmoRatio < 1.0f;
            if (hasFired == true)
            {
                Debug.Log("HIT TRUE");
                IsShooting = true;
            }
            else
            {
                IsShooting = false;
            }
        }
        var ammoRatio = (int)Math.Round(activeThing.CurrentAmmoRatio * 100);

        if (ammoRatio >= 0)
        {
            Ammo = ammoRatio;
        }
        else
        {
            Ammo = 0;
        }
    }

    void OnEnemyKilled(EnemyKillEvent evt)
    {
        EnemiesLeft = evt.RemainingEnemyCount;
    }

    private void Awake()
    {
        actorsManager = FindObjectOfType<ActorsManager>();
        weaponManager = FindObjectOfType<PlayerWeaponsManager>();
        m_InputHandler = GetComponent<PlayerInputHandler>();
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
}

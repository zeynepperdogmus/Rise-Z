using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private void Awake()
    {
        instance = this;
    }
    // public Transform WeaponParent;


    public Animator Animation;

    [SerializeField] private WeaponVariables WeaponSlot1;
    [SerializeField] private WeaponVariables WeaponSlot2;
    [SerializeField] private WeaponVariables CurrentWeapon;

    [SerializeField] private int maxAmmo=25;
    [SerializeField] private int maxMagAmmo=14;
    [SerializeField] private int currentAmmo;

    [SerializeField] private int damage = 10;
    [SerializeField] private float fireFreq = 0.1f;
    private float fireCounter;
    Camera cam;
    bool isReload, isFire;
    private void Start()
    {
        cam = Camera.main;

       CurrentWeapon = WeaponSlot1;
        ChangeWeapon(WeaponSlot1);
 
    }
    private void Update()
    {
        Inputs();
    }
    private void Inputs()
    {
        if (Input.GetMouseButton(0) && Time.time > fireCounter&& currentAmmo>0&&!isReload&&!isFire)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R)&&!isReload)
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)&& CurrentWeapon!=WeaponSlot1)
        {
            isReload = false;
            isFire = false;
            ChangeWeapon(WeaponSlot1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && CurrentWeapon != WeaponSlot2)
        {
            isReload = false;
            isFire = false;
            ChangeWeapon(WeaponSlot2);
        }
    }
    private void Fire()
    {
        isFire=true;
        Animation.Play("Fire");
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 100f))
        {
         
            Debug.Log(hit.collider.name);
            IZombieHit zombieHit = hit.collider.GetComponent<IZombieHit>();
            if (zombieHit != null)
            {
                zombieHit.DealDamage(damage);
            }
            currentAmmo -= 1;
            fireCounter = Time.time + fireFreq;
        }
    }
   private void Reload()
    {
        if (currentAmmo!=maxMagAmmo&&maxAmmo>0)
        {
            isReload=true;
            Animation.Play("Reload");
      
        }       
    }
    public void EndFire()
    {
        isFire = false;
    }
    public void EndReload()
    {
        maxAmmo += currentAmmo;
        currentAmmo = 0;
        if (maxAmmo >= maxMagAmmo)
        {
            currentAmmo = maxMagAmmo;
            maxAmmo -= maxMagAmmo;
        }
        else
        {
            currentAmmo = maxAmmo;
            maxAmmo = 0;
        }
        isReload = false;
    }
    private void ChangeWeapon(WeaponVariables Weapon)
    {
      CurrentWeapon.transform.gameObject.SetActive(false);
        CurrentWeapon.GetComponent<WeaponVariables>().CurrentAmmo = currentAmmo;
        CurrentWeapon = Weapon;
        CurrentWeapon.transform.gameObject.SetActive(true);
        Animation = CurrentWeapon.Animation;
        fireFreq = CurrentWeapon.FireFreq;
        currentAmmo = CurrentWeapon.CurrentAmmo;
        maxAmmo=CurrentWeapon.MaxAmmo;
        maxMagAmmo = CurrentWeapon.MaxMagAmmo;
    }
}

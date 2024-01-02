using UnityEngine;

public class AnimController : MonoBehaviour
{
    public void EndReload()
    {
        WeaponManager.instance.EndReload();
    }
    public void EndFire()
    {
        WeaponManager.instance.EndFire();
    }
}

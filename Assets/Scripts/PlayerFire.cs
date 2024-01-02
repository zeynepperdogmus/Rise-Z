using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private int damage=10;    
    [SerializeField] private float fireFreq=0.1f;    
    private float fireCounter;    
    Camera cam;
    private void Start()
    {
        cam=Camera.main;
    }
    private void Update()
    {
            Inputs();         
    }
    private void Inputs()
    {
        if (Input.GetMouseButton(0)&&Time.time>fireCounter)
        {
            Fire();
        }
    }
    private void Fire()
    {      
        if (Physics.Raycast(cam.transform.position,cam.transform.forward, out RaycastHit hit,100f))
        {
            Debug.Log(hit.collider.name);
            IZombieHit zombieHit = hit.collider.GetComponent<IZombieHit>();
            if (zombieHit != null)
            {
                zombieHit.DealDamage(damage);
            }   
            fireCounter=Time.time+fireFreq;
        }
    } 
}

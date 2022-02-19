using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private GameObject tBullet;

    private float fireRate;
    private bool canFire;
    private float dmg;
    private float sSpeed;

    private TowerLookAt tla;
    
    // Start is called before the first frame update
    void Start()
    {
        Tower tow = gameObject.GetComponentInParent<Tower>();
        dmg = tow.damage;
        fireRate = tow.fireRate;
        sSpeed = tow.shotSpeed;

        tla = gameObject.GetComponentInParent<TowerLookAt>();
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tla.EnemiesInRange())
        {
            if (canFire) { StartCoroutine(Fire()); }
        }
    }

    IEnumerator Fire()
    {
        canFire = false;
        tBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        tBullet.GetComponent<BulletScript>().SetDamage(dmg);
        tBullet.GetComponent<BulletScript>().SetShotSpeed(sSpeed);

        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }
}

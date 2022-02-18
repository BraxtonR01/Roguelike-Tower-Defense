using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private GameObject tempBullet;

    [SerializeField] float fireRate;
    private bool canFire;

    private GameObject target;

    private TowerLookAt tla;
    
    // Start is called before the first frame update
    void Start()
    {
        tla = GetComponentInParent<TowerLookAt>();
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tla.EnemiesInRange())
        {
            target = tla.GetTarget().gameObject;
            if (canFire) { StartCoroutine(Fire()); }
        }
    }

    IEnumerator Fire()
    {
        canFire = false;
        tempBullet = Instantiate(bullet, gameObject.transform);

        yield return new WaitForSeconds(1/fireRate);
        canFire = true;
    }
}

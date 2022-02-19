using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    private float dmg;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("Lifespan");
        rb.AddForce(rb.transform.up * speed*10);
    }

    public void SetShotSpeed(float sSpeed)
    {
        speed = sSpeed;
    }

    public void SetDamage(float damage)
    {
        dmg = damage;
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(dmg);
        }

        Destroy(gameObject);
    }
}

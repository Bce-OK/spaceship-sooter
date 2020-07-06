using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed;

    [SerializeField] private float _damage;

    [SerializeField] private float lifeTime;

    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private float   distance;
    
    [SerializeField] public ParticleSystem crashEffect;
    
    private Rigidbody2D _rb;

    void Start()
    {
        if (lifeTime <= 0)
            lifeTime = 4;
        if (_speed <= 1.0f)
            _speed = 1.0f;
        _damage = 1f;
        distance = 1;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.up * _speed;
        if (whatIsSolid.value == 0)
            whatIsSolid = LayerMask.GetMask("Solid");
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            return ;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,
            Vector2.up,
            distance,
            whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(_damage);
            }
            Instantiate(crashEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        _rb.position += _rb.velocity * (Time.deltaTime * _speed);
        lifeTime -= Time.deltaTime;
    }
}

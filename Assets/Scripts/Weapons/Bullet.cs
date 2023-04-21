using System;
using MatteoBenaissaLibrary.SpriteView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public LayerMask WallLayer;

    public SpriteView _spriteView;

    private void Start()
    {
        _spriteView = GetComponent<SpriteView>();
        _spriteView.PlayState("Idle");
    }
    

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Contains(EnemyLayer, other.gameObject.layer))
        {
            other.GetComponent<Enemy>().TakeDamage();
            _spriteView.PlayAction("Hit");
            _spriteView.OnActionEnd.AddListener(DestroyHimself);
        }

        if (Contains(WallLayer, other.gameObject.layer))
        {
            print("baka");
            _spriteView.PlayAction("Hit");
            _spriteView.OnActionEnd.AddListener(DestroyHimself);
        }
    }

    private void DestroyHimself()
    {
        Destroy(gameObject);
    }
}
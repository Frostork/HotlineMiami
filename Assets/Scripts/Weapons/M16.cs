using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class M16 : MonoBehaviour
{
    public GameObject Bullet;
    public float Speed;
    public int MunitStack;
    
    public void Attack()
    {
        if (MunitStack > 0)
        {
            GameObject actualBullet = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            MunitStack--;
        }
        else
        {
            return;
        }
    }
}
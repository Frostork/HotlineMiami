using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public WeaponType type;

    public enum WeaponType
    {
        Axe,
        M16,
        Other
    }

    public struct Type
    {
        private WeaponType types;
    }

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Contains(PlayerLayer, other.gameObject.layer))
        {
            CharacterController _characterController = other.GetComponent<CharacterController>();

            _characterController._actualCollectable = this;
            
            switch (type)
            {
                case WeaponType.Axe:
                    if (_characterController.CanTakeWeapon)
                    {
                        _characterController._haveAxe = true;
                        Destroy(gameObject);
                    }

                    break;

                case WeaponType.M16:
                    if (_characterController.CanTakeWeapon)
                    {
                        _characterController._haveM16 = true;
                        Destroy(gameObject);
                    }

                    break;

                case WeaponType.Other:
                    print("Autre objet !");
                    break;
            }
        }
    }
}
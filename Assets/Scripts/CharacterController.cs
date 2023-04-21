using MatteoBenaissaLibrary.SpriteView;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public bool _haveAxe;
    public bool _haveM16;
    public bool CanTakeWeapon;

    private Vector3 moveDirection;

    private SpriteView _spriteView;

    [SerializeField] private bool CanAttack;
    [SerializeField] private GameObject CaCBox;
    [SerializeField] private GameObject M16Origin;

    [SerializeField] private GameObject M16Prefab;
    [SerializeField] private GameObject AxePrefab;

    public  Collectable _actualCollectable;
    public bool CanPick;
    
    private void Start()
    {
        _spriteView = GetComponent<SpriteView>();
        CanAttack = true;
    }

    private void Update()
    {
        Animate();

        CheckWeapon();

        if (_actualCollectable != null)
            CanPick = true;
        
        if (Input.GetMouseButton(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(_haveAxe || _haveM16)
                StartThrow();

            else if(CanPick)
            {
                
            }
        }
    }

    private void CheckWeapon()
    {
        if (_haveAxe || _haveM16)
        {
            CanTakeWeapon = false;
        }
        else
        {
            CanTakeWeapon = true;
        }
    }

    private void Animate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (_haveAxe == false && _haveM16 == false)
                _spriteView.PlayState("WalkUnarmed");
            else
            {
                if (_haveAxe)
                    _spriteView.PlayState("WalkAxe");
                else
                {
                    _spriteView.PlayState("WalkM16");
                }
            }
        }
        else
        {
            if (_haveAxe == false && _haveM16 == false)
                _spriteView.PlayState("Idle");
            else
            {
                if (_haveAxe)
                    _spriteView.PlayState("IdleAxe");
                else
                {
                    _spriteView.PlayState("IdleM16");
                }
            }
        }

        moveDirection.x = horizontalInput * MoveSpeed * Time.deltaTime;
        moveDirection.y = verticalInput * MoveSpeed * Time.deltaTime;

        transform.position += moveDirection;
    }

    private void Attack()
    {
        if (CanAttack == true)
        {
            CanAttack = false;
            if (_haveAxe)
            {
                _spriteView.PlayAction("AttackAxe");
                _spriteView.OnActionEnd.AddListener(ResetAttack);
                CaCBox.GetComponent<Axe>().Attack();
            }
            else if (_haveM16)
            {
                _spriteView.PlayAction("AttackM16");
                _spriteView.OnActionEnd.AddListener(ResetAttack);
                M16Origin.GetComponent<M16>().Attack();
            }
            else
            {
                CanAttack = true;
            }
        }
    }

    private void StartThrow()
    {
        if (_haveAxe)
        {
            _spriteView.PlayAction("Throw");
            _spriteView.OnActionEnd.AddListener(Throw);
        }
        else if (_haveM16)
        {
            _spriteView.PlayAction("Throw");
            _spriteView.OnActionEnd.AddListener(Throw);
        }
    }

    private void Throw()
    {
        if (_haveAxe)
        {
         
            _haveAxe = false;
        }

        if (_haveM16)
        {
            
            _haveM16 = false;
        }
    }
    
    private void ResetAttack()
    {
        CanAttack = true;
    }
}
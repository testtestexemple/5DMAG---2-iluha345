using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _targetZone;

    [Header("Stats")]
    public PlayerStat Statistics;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _chController;


    private bool _forward;
    private float _velocityDown;
    private Vector2 _inputMovement;
    public Vector3 _direction;
    private WeaponModel _weapon;

    private static PlayerController _instance;

    public static PlayerController Instance { get { return _instance; } }

    public AudioSource hitSound;
    public AudioSource miss;

    public bool IsCanMoving{ private get; set; }
    public bool IsCanAttack { private get; set; }
    private bool criticalDamage;

    private float actMeter;

    public GameObject DeadScreen;
    private GameStat stat;

    private void Awake()
    {
        _instance = this;
        _weapon = new SwordModel();
        Statistics.SetDefValue();
    }

    private void Start()
    {
        _forward = true;
        IsCanAttack = true;
        stat = GameObject.Find("Game Manager").GetComponent<GameStat>();
        _direction = Vector3.zero;
        //IF SOMEONE FUCKING TAKES vSYNC OUT OF HERE -BITCH I'LL RIP OFF YOUR FUCKING HANDS !!!!!
        QualitySettings.vSyncCount = 1;
        //DO NOT TOUCH !!
        actMeter = transform.position.z;
    }

    private void Update()
    {
        if(IsCanMoving == false)
            return;

        ApplyGravity();
        ApplyMovement();
        MeterCouner();
        if (Statistics.Health <= 0)
            GameOver();
    }
    private void MeterCouner()
    {
        if (transform.position.z > actMeter + 2 || transform.position.z < actMeter - 2)
        {
            actMeter = transform.position.z;
            stat.allDistance++;
        }
    }

    private void GameOver()
    {
        IsCanMoving = false;
        DeadScreen.SetActive(true);
        stat.SetStat();
        IsCanMoving = false;
        _direction = Vector3.zero;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_targetZone.position, _targetZone.position + _targetZone.position * 0);
        Gizmos.DrawWireSphere(_targetZone.position + _targetZone.position * 0, 1);
    }

    public void ChangeMovement(Vector2 movement)
    {
        if (!IsCanMoving) return;

        _inputMovement = movement;
    }

    public WeaponModel GetWeapon()
    {
        return _weapon;
    }

    private void ApplyGravity()
    {
        if (!IsCanMoving) return;

        if (_chController.isGrounded && _velocityDown < 0.0f)
        {
            _velocityDown = -1.0f;
        }
        else
        {
            _velocityDown += Physics.gravity.y * Statistics.Speed * Time.deltaTime;
        }

        _direction.y = _velocityDown;
    }

    private void ApplyMovement()
    {
        if (!IsCanMoving) return;//блять чого він рухається коли IsCanMoving==false

        _direction.z = _inputMovement.x;

        _chController.Move(_direction * Statistics.Speed * Time.deltaTime);

        _animator.SetFloat("speed", _inputMovement.x);

        if (_inputMovement.x > 0)
        {
            if (!_forward)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _forward = true;
            }
            
        }
        else if (_inputMovement.x < 0)
        {
            if (_forward)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _forward = false;
            }
            
        }
    }

    public void ToAttack()
    {
        if (!IsCanMoving) return;
        if (!IsCanAttack) return;
        IsCanAttack = false;
        StartCoroutine("AttackDelay");

        stat.allHits++;
        PlayAttackAnimation();
    }
    public void GiveDamage()
    {
        RaycastHit[] hist;
        hist = Physics.SphereCastAll(_targetZone.position, 1f, _targetZone.position, 0);

        foreach (RaycastHit hit in hist)
        {
            var enemy = hit.collider.GetComponent<EnemyController>();
            if (enemy)
            {
                stat.allNoMissedHits++;
                if (criticalDamage)
                { 
                    stat.allCritycalHits++;
                    enemy.ClaimDamage(_weapon.Damage * 2 + Statistics.AdditionalDamage);
                }
                else
                    enemy.ClaimDamage(_weapon.Damage + Statistics.AdditionalDamage);
                    

                hitSound.Play();
                break;
            }
            else
            {
                miss.Play();
            }
        }
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(Statistics.AttackDelay);
        IsCanAttack = true;
    }

    public void ClaimDamage(int damage)
    {
        Statistics.Health -= damage;
        UIController.Instance.UpdateHPUI(Statistics.Health);
    }

    public void ClaimMoney(int reward)
    {
        Statistics.Money += (int)(reward * Statistics.MoneyMultipler);
        UIController.Instance.UpdateCoinText(Statistics.Money);
    }

    private void PlayAttackAnimation()
    {
        if (_inputMovement.x == 0)
        {
            criticalDamage = false;
            _animator.SetBool("atack_1", true);
            _animator.SetBool("atack_2", false);
        }
        else if (_inputMovement.x != 0)
        {
            criticalDamage = true;
            _animator.SetBool("atack_1", false);
            _animator.SetBool("atack_2", true);
        }
    }
}

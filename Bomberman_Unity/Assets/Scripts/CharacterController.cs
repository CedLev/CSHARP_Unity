using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public CharacterSO _CharSO;

    private Animator _Animator;
    public GameObject _Bomb;

    private const float TILE_SIZE = 0.64f;
    private bool _IsMoving;
    private Vector3 _InitPosition;
    private Vector3 _WantedPosition;
    private float _PercentageCompletion;

    private float _MoveSpeed;
    public int _Hp;

    public Image _Hp1;
    public Image _Hp2;
    public Image _Hp3;

    private int _PlayerCol = 1;
    private int _PlayerRow = 1;
    public int _BombCol;
    public int _BombRow;

    public bool _IsAlive = true;
    private bool _CanBomb = true;

    private void Start()
    {
        _Animator = GetComponent<Animator>();

        _Hp = _CharSO.hitPoint;
        _MoveSpeed = _CharSO.moveSpeed;
    }


    private void Update()
    {
        DropBombMotherfucker();
        MovementHorizontal();
        MovementVertical();
    }


    private void MovementHorizontal()
    {
        if (!_IsMoving)
        {
            int askMoveHorizontal = (int)Input.GetAxisRaw("Horizontal");

            if (askMoveHorizontal != 0)
            {
                if (GameManager.Instance._LevelDisposition[_PlayerCol, _PlayerRow + askMoveHorizontal] == ETileType.Floor)
                {
                    _IsMoving = true;
                    _PercentageCompletion = 0;

                    if (askMoveHorizontal == 1)
                        _Animator.SetInteger("checkMovement", 3);
                    else if (askMoveHorizontal == -1)
                        _Animator.SetInteger("checkMovement", 4);

                    _InitPosition = transform.position;

                    Vector3 offset = Vector3.right * askMoveHorizontal * TILE_SIZE;
                    _WantedPosition = _InitPosition + offset;

                    _PlayerRow += askMoveHorizontal;
                }
            }
        }
        Movement();
    }


    private void MovementVertical()
    {
        if (!_IsMoving)
        {
            int askMoveVertical = (int)Input.GetAxisRaw("Vertical");

            if (askMoveVertical != 0)
            {
                if (GameManager.Instance._LevelDisposition[_PlayerCol - askMoveVertical, _PlayerRow] == ETileType.Floor)
                {
                    _IsMoving = true;
                    _PercentageCompletion = 0;

                    if (askMoveVertical == 1)
                        _Animator.SetInteger("checkMovement", 1);
                    else _Animator.SetInteger("checkMovement", 2);

                    _InitPosition = transform.position;

                    Vector3 offset = Vector3.up * askMoveVertical * TILE_SIZE;
                    _WantedPosition = _InitPosition + offset;

                    _PlayerCol += -askMoveVertical;
                }
            }
        }
        Movement();
    }


    private void Movement()
    {
        if (_IsMoving)
        {
            _PercentageCompletion += Time.deltaTime * _MoveSpeed;
            _PercentageCompletion = Mathf.Clamp(_PercentageCompletion, 0, 1);

            transform.position = Vector3.Lerp(transform.position, _WantedPosition, _PercentageCompletion);

            if (Vector3.Distance(transform.position, _WantedPosition) == 0f)
            {
                _IsMoving = false;
                _Animator.SetInteger("checkMovement", 0);
            }
        }
    }


    public void DropBombMotherfucker()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_IsMoving && _CanBomb)
            {
                StartCoroutine(BombTime());

                GameObject bomb = Instantiate(_Bomb);
                bomb.transform.position = transform.position;

                bomb.GetComponent<Bomb>()._BombRow = _PlayerRow;
                bomb.GetComponent<Bomb>()._BombCol = _PlayerCol;
            }
        }
    }


    public IEnumerator BombTime()
    {
        _CanBomb = false;

        yield return new WaitForSeconds(3.6f);

        _CanBomb = true;
    }


}

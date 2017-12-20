using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bomb : MonoBehaviour
{

    public Animator _BombAnimator;
    private const float TILE_SIZE = 0.64f;

    public GameObject _FireDownEnd;
    public GameObject _FireUpEnd;
    public GameObject _FireRightEnd;
    public GameObject _FireLeftEnd;
    public GameObject _FireStart;
    public GameObject _Floor;

    public CharacterController _Player;

    public int _BombRow;
    public int _BombCol;

    void Start()
    {
        _BombAnimator = GetComponent<Animator>();

        Invoke("InstantiateFire", 3f);

        StartCoroutine(DestroyBomb(gameObject));
    }


    private void InstantiateFire()
    {
        FireUp();
        FireDown();
        FireRight();
        FireLeft();
        FireStart();
    }


    private void FireUp()
    {
        if (GameManager.Instance._LevelDisposition[_BombCol - 1, _BombRow] == ETileType.Floor)
        {
            GameObject fireUp = Instantiate(_FireUpEnd);
            Vector2 firePos = transform.position;
            firePos.y += TILE_SIZE;

            fireUp.transform.position = firePos;

            StartCoroutine(DestroyRoutine(fireUp));

            Vector2 origin = fireUp.transform.position;
            Vector2 direction = Vector2.up;

            RaycastHit2D hitPlayer = Physics2D.Raycast(origin, direction, TILE_SIZE/2, LayerMask.GetMask("Character"));

            if(hitPlayer.collider != null)
            {
                GameManager.Instance.PlayerHpLost();
            }

            RaycastHit2D hitEnemy = Physics2D.Raycast(origin, direction, TILE_SIZE / 2, LayerMask.GetMask("Enemy"));

            if (hitEnemy.collider != null)
            {
                Destroy(hitEnemy.collider.gameObject);
            }

        }
        else if (GameManager.Instance._LevelDisposition[_BombCol - 1, _BombRow] == ETileType.Breakable)
        {
            GameManager.Instance._LevelDisposition[_BombCol - 1, _BombRow] = ETileType.Floor;

            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + TILE_SIZE);

            GameManager.Instance.PlaceFloor(_BombCol - 1, _BombRow, spawnPosition);
        }


    }


    private void FireDown()
    {
        if (GameManager.Instance._LevelDisposition[_BombCol + 1, _BombRow] == ETileType.Floor)
        {
            GameObject fireDown = Instantiate(_FireDownEnd);
            Vector2 firePos = transform.position;
            firePos.y -= TILE_SIZE;

            fireDown.transform.position = firePos;

            StartCoroutine(DestroyRoutine(fireDown));

            Vector2 origin = fireDown.transform.position;
            Vector2 direction = Vector2.down;

            RaycastHit2D hitPlayer = Physics2D.Raycast(origin, direction, TILE_SIZE/2, LayerMask.GetMask("Character"));

            if (hitPlayer.collider != null)
            {
                GameManager.Instance.PlayerHpLost();
            }

            RaycastHit2D hitEnemy = Physics2D.Raycast(origin, direction, TILE_SIZE / 2, LayerMask.GetMask("Enemy"));

            if (hitEnemy.collider != null)
            {
                Destroy(hitEnemy.collider.gameObject);
            }
        }
        else if (GameManager.Instance._LevelDisposition[_BombCol + 1, _BombRow] == ETileType.Breakable)
        {
            GameManager.Instance._LevelDisposition[_BombCol + 1, _BombRow] = ETileType.Floor;

            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - TILE_SIZE);

            GameManager.Instance.PlaceFloor(_BombCol + 1, _BombRow, spawnPosition);
        }



    }


    private void FireRight()
    {
        if (GameManager.Instance._LevelDisposition[_BombCol, _BombRow + 1] == ETileType.Floor)
        {
            GameObject fireRight = Instantiate(_FireRightEnd);
            Vector2 firePos = transform.position;
            firePos.x += TILE_SIZE;

            fireRight.transform.position = firePos;

            StartCoroutine(DestroyRoutine(fireRight));

            Vector2 origin = fireRight.transform.position;
            Vector2 direction = Vector2.right;

            RaycastHit2D hitPlayer = Physics2D.Raycast(origin, direction, TILE_SIZE/2, LayerMask.GetMask("Character"));

            if (hitPlayer.collider != null)
            {
                GameManager.Instance.PlayerHpLost();
            }

            RaycastHit2D hitEnemy = Physics2D.Raycast(origin, direction, TILE_SIZE / 2, LayerMask.GetMask("Enemy"));

            if (hitEnemy.collider != null)
            {
                Destroy(hitEnemy.collider.gameObject);
            }
        }
        else if (GameManager.Instance._LevelDisposition[_BombCol, _BombRow + 1] == ETileType.Breakable)
        {
            GameManager.Instance._LevelDisposition[_BombCol, _BombRow + 1] = ETileType.Floor;

            Vector2 spawnPosition = new Vector2(transform.position.x + TILE_SIZE, transform.position.y);

            GameManager.Instance.PlaceFloor(_BombCol, _BombRow + 1, spawnPosition);
        }
    }


    private void FireLeft()
    {
        if (GameManager.Instance._LevelDisposition[_BombCol, _BombRow - 1] == ETileType.Floor)
        {

            GameObject fireLeft = Instantiate(_FireLeftEnd);
            Vector2 firePos = transform.position;
            firePos.x -= TILE_SIZE;

            fireLeft.transform.position = firePos;


            Vector2 origin = fireLeft.transform.position;
            Vector2 direction = Vector2.left;

            RaycastHit2D hitPlayer = Physics2D.Raycast(origin, direction, TILE_SIZE/2, LayerMask.GetMask("Character"));

            if (hitPlayer.collider != null)
            {
                GameManager.Instance.PlayerHpLost();
            }


            RaycastHit2D hitEnemy = Physics2D.Raycast(origin, direction, TILE_SIZE / 2, LayerMask.GetMask("Enemy"));

            if(hitEnemy.collider != null)
            {
                Destroy(hitEnemy.collider.gameObject);
            }


            StartCoroutine(DestroyRoutine(fireLeft));
        }
        else if (GameManager.Instance._LevelDisposition[_BombCol, _BombRow - 1] == ETileType.Breakable)
        {
            GameManager.Instance._LevelDisposition[_BombCol, _BombRow - 1] = ETileType.Floor;

            Vector2 spawnPosition = new Vector2(transform.position.x - TILE_SIZE, transform.position.y);

            GameManager.Instance.PlaceFloor(_BombCol, _BombRow - 1, spawnPosition);


        }
    }

    private void FireStart()
    {
        GameObject fireStart = Instantiate(_FireStart);

        fireStart.transform.position = transform.position;

        StartCoroutine(DestroyRoutine(fireStart));

        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;

        RaycastHit2D hitPlayer = Physics2D.Raycast(origin, direction, TILE_SIZE, LayerMask.GetMask("Character"));

        if (hitPlayer.collider != null)
        {
            GameManager.Instance.PlayerHpLost();
        }

        RaycastHit2D hitEnemy = Physics2D.Raycast(origin, direction, TILE_SIZE / 2, LayerMask.GetMask("Enemy"));

        if (hitEnemy.collider != null)
        {
            Destroy(hitEnemy.collider.gameObject);
        }
    }


    public IEnumerator DestroyEnemy(Collider2D aCol)
    {
        aCol.gameObject.GetComponent<Animator>().SetInteger("CheckEnemyState", 5);
        

        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.EnemyCounter();

        Destroy(aCol.gameObject);
    }

    public IEnumerator DestroyRoutine(GameObject aFire)
    {

        yield return new WaitForSeconds(0.25f);

        Destroy(aFire);
    }

    public IEnumerator DestroyBomb(GameObject aBomb)
    {

        yield return new WaitForSeconds(3.275f);

        Destroy(aBomb);
    }

}

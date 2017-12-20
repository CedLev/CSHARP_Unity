using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager Instance { get { return _Instance; } }

    private const float TILE_SIZE = 64;
    private const int PIXEL_PER_UNIT = 100;

    public GameObject _Floor;
    public GameObject _Wall;
    public GameObject _Breakable;
    public GameObject _Enemy;

    public int _EnemyCount = 0;

    public CharacterController _Player;

    public ETileType[,] _LevelDisposition = new ETileType[20, 20];


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else Destroy(_Instance);
    }

    private void Start()
    {
        TileGenerator();
    }


    public void TileGenerator()
    {
        int lenght = (int)(Screen.width / TILE_SIZE);

        Vector2 initPosition = new Vector2((-Screen.width / 2 + TILE_SIZE / 2) / PIXEL_PER_UNIT, (Screen.height / 2 - TILE_SIZE / 2) / PIXEL_PER_UNIT);

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                Vector2 spawnPosition = initPosition + new Vector2(TILE_SIZE * j / PIXEL_PER_UNIT, -TILE_SIZE * i / PIXEL_PER_UNIT);

                if (i == 0 || j == 0 || i == lenght - 1 || j == lenght - 1)
                {
                    PlaceWall(i, j, spawnPosition);
                }
                else if (i == 1 || j == 1 || i == lenght - 2 || j == lenght - 2)
                {
                    PlaceFloor(i, j, spawnPosition);
                }
                else
                {
                    int random = Random.Range(0, 4);

                    if (random == 0)
                    {
                        PlaceWall(i, j, spawnPosition);
                    }
                    else if (random == 1 || random == 2)
                    {
                        PlaceFloor(i, j, spawnPosition);
                        int randomEnemy = Random.Range(1, 5);

                        if (randomEnemy == 3)
                        {
                            GameObject enemy = Instantiate(_Enemy);
                            enemy.transform.position = spawnPosition;
                            _EnemyCount++;
                        }
                    }
                    else
                    {
                        PlaceBreakable(i, j, spawnPosition);
                    }
                }
            }
        }
    }


    public void PlaceWall(int i, int j, Vector2 spawnPosition)
    {
        GameObject wall = Instantiate(_Wall);
        wall.transform.position = spawnPosition;
        _LevelDisposition[i, j] = ETileType.Wall;
    }


    public void PlaceFloor(int i, int j, Vector2 spawnPosition)
    {
        GameObject floor = Instantiate(_Floor);
        floor.transform.position = spawnPosition;
        _LevelDisposition[i, j] = ETileType.Floor;
    }


    public void PlaceBreakable(int i, int j, Vector2 spawnPosition)
    {
        GameObject floor = Instantiate(_Breakable);
        floor.transform.position = spawnPosition;
        _LevelDisposition[i, j] = ETileType.Breakable;
    }


    public void PlayerHpLost()
    {
        _Player._Hp--;

        if (_Player._Hp == 2)
            _Player._Hp3.gameObject.SetActive(false);
        else if (_Player._Hp == 1)
            _Player._Hp2.gameObject.SetActive(false);
        else if (_Player._Hp <= 0)
        {
            _Player._Hp1.gameObject.SetActive(false);

            SceneManager.LoadScene("LoseMenu");
        }
    }


    public void EnemyCounter()
    {
        _EnemyCount--;

        if(_EnemyCount <= 0)
        {
            SceneManager.LoadScene("LoseMenu");
        }
    }

}





public enum ETileType
{
    Floor,
    Wall,
    Breakable,
}
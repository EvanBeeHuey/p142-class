using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;
    public event Action<Player> OnPlayerSpawned;
    public UnityEvent<int> OnLifeValueChanged;
    public UnityEvent<int> OnScoreValueChanged;

    #region GAME PROPERTIES
    //lives
    [SerializeField] private int maxLives = 5;
    private int _lives = 3;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                GameOver();
                return;
            }

            if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"{gameObject.name} lives has changed to {_lives}");
        }
    }
    #endregion

    #region PLAYER CONTROLLER INFO
    [SerializeField] private Player playerPrefab;
    private Player _playerInstance;
    public Player PlayerInstance => _playerInstance;
    #endregion

    private MenuController currentMenuController;
    private Transform currentCheckpoint;

    public void SetMenuController(MenuController newMenuController) => currentMenuController = newMenuController;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        if (maxLives <= 0) maxLives = 5;
        _lives = maxLives;
    }

    void Update()
    {

    }

    void GameOver()
    {
        Debug.Log("Game Over goes here");
    }

    void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void InstantiatePlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }    
}

using DataStorage;
using Game;
using Tools.Config;
using UI;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    public string configPath;

    private IConfigLoader _configLoader;
    private IDataStorage<Data> _dataStorage;

    [SerializeField]
    private PlayerStatsPresenter[] _players;

    [SerializeField]
    private CameraController _cameraController;

    private GameLogic _game;
    
    private void Awake()
    {
        _configLoader = new ConfigLoader();
        _dataStorage = new DataStorage.DataStorage(_configLoader);
        _dataStorage.Load(configPath);
        
        _cameraController.Init(_dataStorage.Data.cameraSettings);

        _game = new GameLogic(_dataStorage.Data);
    }

    private void Start()
    {
        RestartWithBuffs();
    }

    public void AttackLeft()
    {
        _game.Attack(0, 1);
        _players[0].Attack();
    }
    
    public void AttackRight()
    {
        _game.Attack(1, 0);
        _players[1].Attack();
    }

    public void RestartWithBuffs()
    {
        Restart(true);
    }
    
    public void RestartWithoutBuffs()
    {
        Restart(false);
    }

    private void Restart(bool allowBuffs)
    {
        _game.Reset();
        _game.Start(_dataStorage.Data.settings.playersCount, allowBuffs);
        
        for (var i = 0; i < _players.Length; i++)
        {
            _players[i].Reset();
            _players[i].Init(_game.GetPlayerStats(i), _game.GetPlayerBuffs(i), _dataStorage.Data);
        }
    }
}
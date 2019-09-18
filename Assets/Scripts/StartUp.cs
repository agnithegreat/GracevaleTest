using DataStorage;
using Presenters;
using Tools.Config;
using UI;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    public string configPath;

    private IConfigLoader _configLoader;
    private IDataStorage<Data> _dataStorage;

    [SerializeField]
    private GameView _gameView;
    private GamePresenter _gamePresenter;

    [SerializeField]
    private PlayerView[] _players;
    private PlayerPresenter[] _presenters;

    [SerializeField]
    private CameraController _cameraController;

    private Game.GameModel _game;
    
    private void Awake()
    {
        _configLoader = new ConfigLoader();
        _dataStorage = new DataStorage.DataStorage(_configLoader);
        _dataStorage.Load(configPath);
        
        _cameraController.Init(_dataStorage.Data.cameraSettings);

        _game = new Game.GameModel(_dataStorage.Data);
        _game.Init(_dataStorage.Data.settings.playersCount);
        
        _gamePresenter = new GamePresenter(_game, _gameView);
        
        _presenters = new PlayerPresenter[_players.Length];
        for (var i = 0; i < _players.Length; i++)
        {
            _presenters[i] = new PlayerPresenter(_game, _game.GetPlayer(i), _players[i]);
        }
    }

    private void Start()
    {
        _game.Start(true);
    }
}
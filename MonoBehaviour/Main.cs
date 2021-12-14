using Assets.Source.Control;
using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    private const int PlayZoneSize = 9;

    private const int Height = PlayZoneSize + (PlayZoneSize - 2);

    [SerializeField] private GridRenderer _gridRenderer;

    [SerializeField] private int _obstacleIndexX;

    [SerializeField] private int _obstacleIndexY;

    private ControllerSwitch _controllerSwitch;

    private GameState _gameState;

    private GridView _gridView;

    private PlayGrid _playGrid;

    private void Awake()
    {
        _gameState = new GameState();

        _gridView = new GridView();

        _playGrid = new PlayGrid(_gridView, PlayZoneSize);

        Player mainPlayer = new Player(_gameState, _playGrid, "Main", 0, Height / 2);

        Player secondPlayer = new Player(_gameState, _playGrid, "Second", Height, Height / 2);

        Controller player = new PlayerController(mainPlayer, _playGrid, _gameState);

        Controller ai = new AIController(secondPlayer, _playGrid, _gameState);

        _controllerSwitch = new ControllerSwitch(_gameState, new Controller[] { player, ai });

        SubscribeOnActions();
    }

    private void Start()
    {
        _gridRenderer.Init(_gridView, _playGrid, PlayZoneSize);

        _playGrid.RefreshGrid();
    }

    private void SubscribeOnActions()
    {
        _gameState.WhenFinish += _playGrid.ClearGrid;

        _gameState.WhenFinish += _playGrid.RefreshGrid;
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        _gameState.GameFinish(null);
    }

    [ContextMenu("Find way")]
    public void FindWay()
    {
        _playGrid.FindWay();
    }

    [ContextMenu("Set obstacle")]
    public void SetObstacle()
    {
        if (_controllerSwitch.IsCurrectControllerInput(ControllerInput.Inspector))
        {
            _controllerSwitch.ActualController.PlayerSetObstacle(_obstacleIndexX, _obstacleIndexY);
        }
    }

    [ContextMenu("Move up")]
    public void PlayerMoveUp()
    {
        if (_controllerSwitch.IsCurrectControllerInput(ControllerInput.Inspector))
            _controllerSwitch.ActualController.PlayerMoveUp();
    }

    [ContextMenu("Move down")]
    public void PlayerMoveDown()
    {
        if (_controllerSwitch.IsCurrectControllerInput(ControllerInput.Inspector))
            _controllerSwitch.ActualController.PlayerMoveDown();
    }

    [ContextMenu("Move right")]
    public void PlayerMoveRight()
    {
        if (_controllerSwitch.IsCurrectControllerInput(ControllerInput.Inspector))
            _controllerSwitch.ActualController.PlayerMoveRight();
    }

    [ContextMenu("Move left")]
    public void PlayerMoveLeft()
    {
        if (_controllerSwitch.IsCurrectControllerInput(ControllerInput.Inspector))
            _controllerSwitch.ActualController.PlayerMoveLeft();
    }
}

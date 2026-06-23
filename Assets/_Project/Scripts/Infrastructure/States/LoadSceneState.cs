using System;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSceneState : IPayloadState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadSceneState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName)
    {
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
    }

    private void OnLoaded()
    {
        InitGameObject();
    }

    private void InitGameObject()
    {
        var mainCanvasPrefab = Resources.Load<GameObject>("MainCanvas/MainCanvas");

        var mainCanvas = UnityEngine.Object.Instantiate(mainCanvasPrefab)
            .GetComponent<MainCanvas>();

        mainCanvas.Construct();
    }

    public void Exit()
    {
        
    }
}
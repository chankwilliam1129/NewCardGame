using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    public enum BattleSceneState
    {
        BATTLE_START,
        PLAYER_TURN_START,
        PLAYER_TURN_UPDATE,
        PLAYER_TURN_END,
        ENEMY_TURN_START,
        ENEMY_TURN_UPDATE,
        ENEMY_TURN_END,
        BATTLE_END,
        MAX,
    }

    public bool isPause;

    public event EventHandler OnBattleStart;

    public event EventHandler OnPlayerTurnStart;

    public event EventHandler OnPlayerTurnEnd;

    public event EventHandler OnEnemyTurnStart;

    public event EventHandler OnEnemyTurnEnd;

    public BattleSceneState nowState;

    public Transform mainGameCanvas;

    [Space]
    public BattleEvent playerTurnStartText;

    [Space]
    public PlayerWinPlane playerWin;

    public GameObject playerLose;
    public GameObject playerGameClear;

    public static BattleStateManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nowState = BattleSceneState.BATTLE_START;
        OnPlayerTurnStart += BattleStateManager_OnPlayerTurnStart;
    }

    private void BattleStateManager_OnPlayerTurnStart(object sender, EventArgs e)
    {
        Debug.Log("Player Turn Start");
        Instantiate(playerTurnStartText, mainGameCanvas);
    }

    private void Update()
    {
        switch (nowState)
        {
            case BattleSceneState.BATTLE_START:
                OnBattleStart?.Invoke(this, EventArgs.Empty);
                nowState = BattleSceneState.PLAYER_TURN_START;
                OnPlayerTurnStart?.Invoke(this, EventArgs.Empty);
                break;

            case BattleSceneState.PLAYER_TURN_START:
                if (!BattleEventManager.Instance.Execute())
                {
                    nowState = BattleSceneState.PLAYER_TURN_UPDATE;
                }
                break;

            case BattleSceneState.PLAYER_TURN_UPDATE:
                break;

            case BattleSceneState.PLAYER_TURN_END:
                if (!BattleEventManager.Instance.Execute())
                {
                    nowState = BattleSceneState.ENEMY_TURN_START;
                    OnEnemyTurnStart?.Invoke(this, EventArgs.Empty);
                }
                break;

            case BattleSceneState.ENEMY_TURN_START:
                if (!BattleEventManager.Instance.Execute())
                {
                    nowState = BattleSceneState.ENEMY_TURN_UPDATE;
                }
                break;

            case BattleSceneState.ENEMY_TURN_UPDATE:
                if (!BattleEventManager.Instance.Execute())
                {
                    nowState = BattleSceneState.ENEMY_TURN_END;
                    OnEnemyTurnEnd?.Invoke(this, EventArgs.Empty);
                }
                break;

            case BattleSceneState.ENEMY_TURN_END:
                if (!BattleEventManager.Instance.Execute())
                {
                    nowState = BattleSceneState.PLAYER_TURN_START;
                    OnPlayerTurnStart?.Invoke(this, EventArgs.Empty);
                }
                break;

            case BattleSceneState.BATTLE_END:

                break;
        }
    }

    public bool IsPlayerTurn()
    {
        return nowState == BattleSceneState.PLAYER_TURN_UPDATE;
    }

    public void PlayerTurnEnd()
    {
        CardEffectExecutor.Instance?.CleaeModCard();
        nowState = BattleSceneState.PLAYER_TURN_END;
        OnPlayerTurnEnd?.Invoke(this, EventArgs.Empty);
    }

    public void SetBattleEnd(bool isPlayerWin)
    {
        if (nowState != BattleSceneState.BATTLE_END)
        {
            BattleEventManager.Instance.Clear();
            nowState = BattleSceneState.BATTLE_END;

            if (isPlayerWin)
            {
                if (MapData.Instance.isLastMap()) Instantiate(playerGameClear, BattleEventManager.Instance.transform);
                else Instantiate(playerWin, BattleEventManager.Instance.transform);
            }
            else Instantiate(playerLose, BattleEventManager.Instance.transform);
        }
    }
}
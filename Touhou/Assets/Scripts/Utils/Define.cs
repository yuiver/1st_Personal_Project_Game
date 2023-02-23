using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    { 
        Unknown,
        LoadingScene,
        TitleScene,
        LevelSelectScene,
        CharaSelectScene,
        GamePlayScene,
    }

    public enum Sound
    { 
        Bgm,
        SE,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
    }
    public enum MouseEvent
    { 
        Press,
        Click,
    } 
    public enum PlayerAttack
    {
        Missile,
        GuidedMissile,
        Bomb,       
    }
    public enum EnemyAttack
    {
        Straight,
        GuidedPos,
        Circle,
        CircularSector,
    }


    public const int CAN_RETRY_COUNT = 3;

    public const string OBJ_POOL_INST_NAME = "@Pool";
    public const string ROOT_INST_NAME = "{0}_Root";
    public const string ENEMY_PREFAB_PATH = "enemy";
    public const string PLAYER_BULLET_PREFAB_PATH = "playerBullet";
    public const string PLAYER_GUIDED_BULLET_PREFAB_PATH = "playerGuidedBullet";
    public const string ENEMY_BULLET_PREFAB_PATH = "enemyBullet";
    public const string BULLET_PREFAB_PATH = "bullet";
}

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
    public enum EnemyType
    {
        common,
        rare,
        boss,    
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

    public const string PLAYER_BULLET_PREFAB_PATH = "playerBullet";
    public const string PLAYER_GUIDED_BULLET_PREFAB_PATH = "playerGuidedBullet";

    public const string ENEMY_PREFAB_PATH = "Stage01/enemy";
    public const string ENEMY_BOSS_STAGE_01 = "Stage01/Stage01Boss";
    public const string ENEMY_BULLET_PREFAB_PATH = "Stage01/enemyBullet";

    public const string ITEM_POINT = "PointItem";
    public const string ITEM_POWER = "PowerItem";

    public const float maxDistX = 400.0f;
    public const float minDistX = -700.0f;
    public const float maxDistY = 550.0f;
    public const float minDistY = -500.0f;
}

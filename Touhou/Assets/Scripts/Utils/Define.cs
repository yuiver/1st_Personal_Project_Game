using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    { 
        Unknown,
        TitleScene,
        GamePlayScene,
    }

    public enum Sound
    { 
        Bgm,
        Effect,
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
    public enum CameraMode
    { 
        QuaterView,
    }
    public enum PlayerAttack
    {
        Missile,
        GuidedMissile,
        Bomb,       
    }
    public enum EnemyAttack
    {
        Square,
        Arrow,
        Circle,
    }

    public const int CAN_RETRY_COUNT = 3;

    public const string OBJ_POOL_INST_NAME = "@Pool";
    public const string ROOT_INST_NAME = "{0}_Root";
    public const string ENEMY_PREFAB_PATH = "enemy";
    public const string MY_BULLET_PREFAB_PATH = "myBullet";
    public const string BULLET_PREFAB_PATH = "bullet";
}

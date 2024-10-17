using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetPathConfig
{
    //资源加载路径总结点
    public const string GAMEDATA = "Assets/GameData/";

    public const string GAME = GAMEDATA+"Game/";

    //游戏内预制体路径
    public const string GAME_PREFABS = GAME + "Prefabs/";
    //游戏内英雄预制体路径
    public const string GAME_PREFABS_HERO = GAME_PREFABS + "Hero/";
    //游戏内怪物预制体路径
    public const string GAME_PREFABS_MONSTER = GAME_PREFABS + "Monster/";

    public const string HALL = "Assets/GameData";
}

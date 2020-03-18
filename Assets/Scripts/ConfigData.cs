using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData
{
    public ConfigData()
    {
        Purdue.Add(69, 4);
        Purdue.Add(89, 5);
        Purdue.Add(109, 7);
        Purdue.Add(129, 7);
        Purdue.Add(159, 8);
        Purdue.Add(175, 8);

    }

    // 阵法列表
    public static StanceBase[] StanceEnumList =
    {
        new NormalStance(),
        new SkyStance(),
        new TigerSance(),
        new DragonStance(),
        new CloudStance(),
        new BirdStance(),
        new EagleStance(),
        new WindStance(),
        new LandStance(),
        new SnakeStance(),
        new ThunderStance(),
    };

    public static int[] LevelList =
    {
        69,89,109,129,159,175
    };

    // 场地格子数
    public static int GridSize = 12;
    // 一个站位的宽度 单位(米)
    public static int CellSize = 1;

    public static Vector3 CameraPos = new Vector3(6f, 9f, 0f);
    public static Quaternion CameraRot = Quaternion.Euler(58.3f, 0f, 0f);

    public static Vector2Int[] PetRanks =
    {
        new Vector2Int(7, 4),
        new Vector2Int(8, 5),
        new Vector2Int(6, 3),
        new Vector2Int(9, 6),
        new Vector2Int(5, 2),
    };

    // 普渡回合数
    public static Dictionary<int, int> Purdue = new Dictionary<int, int>();

    // 位置默认血量
    public static int[] DefaultHp = { 5200, 6300, 7500, 8000 };

    public static int MaxPetCount = 8;
}

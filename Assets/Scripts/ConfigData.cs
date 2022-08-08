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

    public static Vector3 CameraTheirPos = new Vector3(4f, 7f, 3.8f);
    public static Quaternion CameraTheirRot = Quaternion.Euler(58f, 0f, 0f);

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
    public static int MaxPrepCount = 20;
    
    /**
     * 默认快捷键
     */
    public static Dictionary<OperateEnum, string> DefaultShortcut = new Dictionary<OperateEnum, string>()
    {
        {OperateEnum.Undo, "LeftControl_Z"},
        {OperateEnum.PetDie_1, "Alpha1"},
        {OperateEnum.PetDie_2, "Alpha2"},
        {OperateEnum.PetDie_3, "Alpha3"},
        {OperateEnum.PetDie_4, "Alpha4"},
        {OperateEnum.PetDie_5, "Alpha5"},
        {OperateEnum.UsePrep_1, "Keypad1"},
        {OperateEnum.UsePrep_2, "Keypad2"},
        {OperateEnum.UsePrep_3, "Keypad3"},
        {OperateEnum.UsePrep_4, "Keypad4"},
        {OperateEnum.UsePrep_5, "Keypad5"},
        {OperateEnum.GuangHui_1, ""},
        {OperateEnum.GuangHui_2, ""},
        {OperateEnum.GuangHui_3, ""},
        {OperateEnum.GuangHui_4, ""},
        {OperateEnum.GuangHui_5, ""},        
        {OperateEnum.PoJia_1, ""},
        {OperateEnum.PoJia_2, ""},
        {OperateEnum.PoJia_3, ""},
        {OperateEnum.PoJia_4, ""},
        {OperateEnum.PoJia_5, ""},
    };
}

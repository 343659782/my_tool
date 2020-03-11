using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils
{
    public static float GetReinforce(StanceEnum stance1, StanceEnum stance2)
    {
        return DataUtils.GetStance(stance1).ReinForce[stance2];
    }

    public static StanceBase GetStance(StanceEnum stance)
    {
        StanceBase res = null;
        for (int i = 0; i < ConfigData.StanceEnumList.Length; i++)
        {
            if (ConfigData.StanceEnumList[i].Type == stance)
            {
                res = ConfigData.StanceEnumList[i];
                break;
            }
        }

        return res;
    }

    public static Vector3 GetPosByRank(int x, int y)
    {
        Vector3 res = Vector3.zero;
        res.x = x * ConfigData.CellSize + ConfigData.CellSize * 0.5f;
        res.z = y * ConfigData.CellSize + ConfigData.CellSize * 0.5f;
        return res;
    }

    public static Vector3 GetPosByRank(Vector2Int rank)
    {
        return DataUtils.GetPosByRank(rank.x,rank.y);
    }

    public static Vector2Int OurRankToTheirRank(int x, int y)
    {
        return new Vector2Int(ConfigData.GridSize - x - 1, ConfigData.GridSize - y - 1);
    }

    public static Vector2 WorldPosToUIPos(Vector3 worldPos,Canvas canvas)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 uiPos = canvas.transform.InverseTransformPoint(screenPoint);
        return uiPos;
    }

    public static Vector2 WorldPosToUIPos(Vector3 worldPos)
    {
        Canvas canvas = MainPanel.Instance.Canvas == null ? GameObject.Find("Canvas").GetComponent<Canvas>() : MainPanel.Instance.Canvas;
        return DataUtils.WorldPosToUIPos(worldPos, canvas);
    }

    public static Vector2 ScreenPosToUIPos(Vector2 screenPoint)
    {
        Canvas canvas = MainPanel.Instance.Canvas == null ? GameObject.Find("Canvas").GetComponent<Canvas>() : MainPanel.Instance.Canvas;
        Vector2 uiPos = canvas.transform.InverseTransformPoint(screenPoint);
        return uiPos;
    }
}

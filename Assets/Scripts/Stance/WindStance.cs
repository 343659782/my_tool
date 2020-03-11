using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindStance : StanceBase
{
    public WindStance()
    {
        _type = StanceEnum.Wind;
        _name = "风阵";
        _reinforce.Add(StanceEnum.Bird, 0.1f);
        _reinforce.Add(StanceEnum.Land, -0.1f);
        _reinforce.Add(StanceEnum.Wind, 0f);
        _reinforce.Add(StanceEnum.Tiger, -0.1f);
        _reinforce.Add(StanceEnum.Cloud, -0.05f);
        _reinforce.Add(StanceEnum.Normal, 0.05f);
        _reinforce.Add(StanceEnum.Sky, 0.1f);
        _reinforce.Add(StanceEnum.Dragon, -0.05f);
        _reinforce.Add(StanceEnum.Snake, 0.05f);
        _reinforce.Add(StanceEnum.Eagle, 0.1f);
        _reinforce.Add(StanceEnum.Thunder, -0.1f);

        this._ranks = new Vector2Int[5];
        this._ranks[0] = new Vector2Int(9, 2);
        this._ranks[1] = new Vector2Int(10, 3);
        this._ranks[2] = new Vector2Int(8, 1);
        this._ranks[3] = new Vector2Int(9, 4);
        this._ranks[4] = new Vector2Int(7, 2);
    }
}

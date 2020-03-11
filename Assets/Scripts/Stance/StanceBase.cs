using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceBase
{
    // 克制关系
    protected Dictionary<StanceEnum, float> _reinforce = new Dictionary<StanceEnum, float>();
    public Dictionary<StanceEnum, float> ReinForce { get { return this._reinforce; } }

    // 类型
    protected StanceEnum _type;
    public StanceEnum Type { get { return this._type; } }

    // 名字
    protected string _name;
    public string Name { get { return this._name; } }

    // 站位行列
    protected Vector2Int[] _ranks;
    public Vector2Int[] Ranks { get { return this._ranks; } }
}

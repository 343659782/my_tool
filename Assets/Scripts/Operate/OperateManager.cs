using System.Collections.Generic;
using UnityEditor;

namespace Operate
{
    public class OperateManager : Singleton<OperateManager>
    {
        public List<IOperate> OperateQueue;

        public IOperate Push(IOperate operate)
        {
            OperateQueue.Add(operate);
            return operate;
        }

        public void Undo()
        {
            if (this.OperateQueue.Count > 0)
            {
                IOperate operate = this.OperateQueue[OperateQueue.Count - 1];
                operate.Undo();
                OperateQueue.RemoveAt(OperateQueue.Count - 1);
            }
        }
        
        public void Redo(){}
        
    }
}
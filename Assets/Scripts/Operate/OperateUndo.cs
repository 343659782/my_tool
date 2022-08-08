namespace Operate
{
    public class OperateUndo: IOperate
    {
        public void Execute()
        {
            OperateManager.Instance.Undo();
        }

        public void Undo()
        {
            OperateManager.Instance.Redo();
        }
    }
}
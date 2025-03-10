using System.Collections.Generic;
using System.Linq;
using Godot;

namespace test.scripts.classes.behaviorTree;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE,
}
public partial class BehaviorNode : RefCounted
{
    protected NodeState state;

    public BehaviorNode parent;
    protected List<BehaviorNode> children = new();

    private Dictionary<string, object> _dataContext = new();
    public BehaviorNode()
    {
        parent = null;
    }

    public BehaviorNode(List<BehaviorNode> children)
    {
        foreach (BehaviorNode child in children)
        {
            _Attach(child);
        }
    }

    public void _Attach(BehaviorNode behaviorNode)
    {
        behaviorNode.parent = this;
        children.Add(behaviorNode);
    }
    
    public virtual NodeState Evaluate() => NodeState.RUNNING;

    public void SetData(string key, object value)
    {
        _dataContext[key] = value;
    }

    public object GetData(string key)
    {
        object value = null;
        if (_dataContext.TryGetValue(key, out value))
            return value;

        BehaviorNode node = parent;
        while (node != null)
        {
            value = node.GetData(key);
            if (value != null)
                return value;
            node = node.parent;
        }

        return null;
    }
    
    public bool ClearData(string key)
    {
        object value = null;
        if (_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }

        BehaviorNode node = parent;
        while (node != null)
        {
            bool cleared = node.ClearData(key);
            if (cleared)
                return true;
            node = node.parent;
        }

        return false;
    }
}
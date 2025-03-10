using Godot;

namespace test.scripts.classes.behaviorTree;

public abstract partial class BehaviorTree : Node
{
    private BehaviorNode _root = null;

    public override void _Ready()
    {
        _root = SetupTree();
    }

    public override void _Process(double delta)
    {
        if (_root != null)
        {
            _root.Evaluate();
        }
    }
        protected abstract BehaviorNode SetupTree();
}
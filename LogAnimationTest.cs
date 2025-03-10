using Godot;
using System;

public partial class LogAnimationTest : Node2D
{
    [Export]
    public Area2D log;

    [Export] public float speed = 10;

    private bool _isDropping = true;
    
    private Vector2 initialPosition;
    private Vector2 upperPosition;
    private Vector2 lowerPosition;

    private float animationTime = 0.4f;
    private float elapsedTime = 0.0f;
    
    
    
    public override void _Ready()
    {
        
        initialPosition = GlobalPosition;
        upperPosition = new Vector2(initialPosition.X + 20, initialPosition.Y - 20);
        lowerPosition = new Vector2(initialPosition.X + 25, initialPosition.Y );
    }

    public override void _Process(double delta)
    {
        if (_isDropping)
        {
            elapsedTime += (float)delta;
            float t = Mathf.Clamp(elapsedTime / animationTime, 0.0f, 1.0f);
            GlobalPosition = DropAnimation(t);
            if (t >= 1f)
            {
                _isDropping = false;
            }
        }
        GD.Print(GlobalPosition);
    }

    public Vector2 DropAnimation(float t)
    {
        // https://docs.godotengine.org/en/stable/tutorials/math/beziers_and_curves.html
        Vector2 q0 = initialPosition.Lerp(upperPosition, t);
        Vector2 q1 = upperPosition.Lerp(lowerPosition, t);
        
        Vector2 r = q0.Lerp(q1, t);
        return r;
    }
}

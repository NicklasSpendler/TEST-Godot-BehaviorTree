using Godot;

public partial class PlayerNpc : CharacterBody2D
{
	[Export] public float Speed { get; set; } = 100f;
	[Export] public Marker2D[] WayPoints;
	
	public Vector2 Destination;
}

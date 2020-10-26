using Godot;
using System;

public class OverworldCharacter : Character
{

    [Export] readonly int SPEED = 150;

    protected Vector2 Velocity;
    protected Vector2 Direction;
    protected Character CollidedCharacter;

    public override void _PhysicsProcess(float delta)
    {
        Move(delta);
    }

    protected void Move(float delta)
    {
        if (Velocity != Vector2.Zero)
        {
            var collision = MoveAndCollide(Velocity * SPEED * delta);

            CollidedCharacter = collision?.Collider as Character;

            AnimStateMachine.Travel("Walk");
            UpdateAnimationStates();
        }
        else
        {
            AnimStateMachine.Travel("Idle");
        }
    }

    private void UpdateAnimationStates()
    {
        AnimTree.Set("parameters/Idle/blend_position", Direction);
        AnimTree.Set("parameters/Walk/blend_position", Direction);
    }
}

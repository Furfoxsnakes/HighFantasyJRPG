using System.Collections.Generic;
using Godot;
using Godot.Collections;
using HighFantasyJRPG.Scripts.ViewModelComponents;

public class Character : KinematicBody2D
{
    protected AnimationPlayer Anim;
    protected AnimationNodeStateMachinePlayback AnimStateMachine;
    protected AnimationTree AnimTree;
    protected Game Game;

    // protected Character CollidedCharacter;
    // protected Vector2 Direction = Vector2.Down;

    // components
    [Export] public CharacterDetails CharacterDetails;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Anim = GetNode<AnimationPlayer>("AnimationPlayer");
        AnimTree = GetNode<AnimationTree>("AnimationTree");
        AnimStateMachine = AnimTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
        AnimTree.Active = true;

        Game = GetTree().Root.GetNode<Game>("Game");
        // GroundMap = Game.GetNode<TileMap>("Ground");
        // RayCastAStar = new RayCastAStar(GroundMap, GetWorld2d().DirectSpaceState);
    }

    // #region Movement

    // protected void Move(float delta)
    // {
    //     if (Velocity != Vector2.Zero)
    //     {
    //         var collision = MoveAndCollide(Velocity * SPEED * delta);

    //         CollidedCharacter = collision?.Collider as Character;

    //         _animStateMachine.Travel("Walk");
    //         UpdateAnimationStates();
    //     }
    //     else
    //     {
    //         _animStateMachine.Travel("Idle");
    //     }
    // }

    // protected void MoveAlongPath(float delta)
    // {
    //     if (Path.Count == 0)
    //     {
    //         _animStateMachine.Travel("Idle");
    //         return;
    //     }

    //     _animStateMachine.Travel("Walk");

    //     var distanceToNext = Position.DistanceTo(Path[0]);

    //     if (distanceToNext > 2.0)
    //     {
    //         //Position = Position.LinearInterpolate(Path[0], Mathf.Min(distance, distanceToNext) / distanceToNext);
    //         Direction = Position.DirectionTo(Path[0]);
    //         Velocity = Direction * SPEED;
    //         Velocity = MoveAndSlide(Velocity);
    //     }
    //     else
    //     {
    //         Path.RemoveAt(0);
    //     }
    // }

    

    // #region Mouse movement

    // protected void MoveToTarget(Vector2 target)
    // {
    //     var exclude = new Array {this};

    //     var results = RayCastAStar.Path(GlobalPosition, target, exclude);

    //     if (results != null)
    //         Path = results;
    //     else
    //         GD.PrintErr($"Couldn't find a path to {target}");
    // }

    // #endregion

    // #endregion
    
}
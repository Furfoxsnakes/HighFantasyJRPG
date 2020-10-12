using Godot;
using System.Collections.Generic;
using Array = Godot.Collections.Array;

public class Character : KinematicBody2D
{
    protected const int SPEED = 150;
    protected Game Game;
    protected Vector2 Velocity;
    protected Vector2 Direction = Vector2.Down;
    protected Character CollidedCharacter;
    protected List<Vector2> Path = new List<Vector2>();
    protected TileMap GroundMap;

    protected RayCastAStar RayCastAStar;

    public Stats Stats;

    private AnimationPlayer _anim;
    private AnimationTree _animTree;
    private AnimationNodeStateMachinePlayback _animStateMachine;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
        _animTree = GetNode<AnimationTree>("AnimationTree");
        _animStateMachine = _animTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

        Game = GetTree().Root.GetNode<Game>("Game");
        GroundMap = Game.GetNode<TileMap>("Ground");
        Stats = GetNode<Stats>("Stats");
        
        RayCastAStar = new RayCastAStar(GroundMap, GetWorld2d().DirectSpaceState);
        
        _animTree.Active = true;
    }

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

            _animStateMachine.Travel("Walk");
            UpdateAnimationStates();
        }
        else
        {
            _animStateMachine.Travel("Idle");
        }
    }

    protected void MoveAlongPath(float delta)
    {
        if (Path.Count == 0)
        {
            _animStateMachine.Travel("Idle");
            return;
        }
        
        _animStateMachine.Travel("Walk");

        var distanceToNext = Position.DistanceTo(Path[0]);

        if (distanceToNext > 2.0)
        {
            //Position = Position.LinearInterpolate(Path[0], Mathf.Min(distance, distanceToNext) / distanceToNext);
            Direction = Position.DirectionTo(Path[0]);
            Velocity = Direction * SPEED;
            Velocity = MoveAndSlide(Velocity);
        }
        else
        {
            Path.RemoveAt(0);
            
        }
    }

    private void UpdateAnimationStates()
    {
        _animTree.Set("parameters/Idle/blend_position", Direction);
        _animTree.Set("parameters/Walk/blend_position", Direction);
    }

    #region Mouse movement

    protected void MoveToTarget(Vector2 target)
    {

        var exclude = new Array {this};

        var results = RayCastAStar.Path(GlobalPosition, target, exclude);

        if (results != null)
            Path = results;
        else
            GD.PrintErr($"Couldn't find a path to {target}");
    }

    #endregion
}

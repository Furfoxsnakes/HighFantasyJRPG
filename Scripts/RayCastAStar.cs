using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public class RayCastAStar
{
    private const int GridSize = 32;
    private System.Collections.Generic.Dictionary<Vector2, Vector2> _cameFrom;
    private Camera2D _camera;
    private Array _excludes;
    private System.Collections.Generic.Dictionary<Vector2, int> _fScore;
    private System.Collections.Generic.Dictionary<Vector2, int> _gScore;
    private readonly int _maxIterations = 1000;
    private List<Vector2> _neighboursMaps = new List<Vector2>();
    private List<Vector2> _openSet;
    private readonly Physics2DDirectSpaceState _space;
    private readonly TileMap _tileMap;

    public RayCastAStar(TileMap tileMap, Physics2DDirectSpaceState space)
    {
        _tileMap = tileMap;
        _space = space;
    }

    /// <summary>
    ///     Finds the point in the F Scores with the smallest value
    /// </summary>
    /// <returns></returns>
    public Vector2 FindSmallestFScore()
    {
        var smallestPoint = Vector2.Zero;
        var smallestScore = int.MaxValue;


        foreach (var point in _openSet)
            if (_fScore[point] != null && _fScore[point] < smallestScore)
            {
                smallestScore = _fScore[point];
                smallestPoint = point;
            }

        return smallestPoint;
    }

    /// <summary>
    ///     Uses raycasting to find if neighbours of provided points are valid
    /// </summary>
    /// <param name="point">
    ///     The Vector2 from which to find neighbours from<</param>
    /// <returns></returns>
    public List<Vector2> GetNeighbours(Vector2 point)
    {
        var targets = new[]
        {
            point + Vector2.Up,
            point + Vector2.Right,
            point + Vector2.Down,
            point + Vector2.Left,
            point + new Vector2(1, 1), //down right
            point + new Vector2(1, -1), // up right
            point + new Vector2(-1, 1), // down left
            point + new Vector2(-1, -1) // up left
        };

        var validTargets = new List<Vector2>();

        foreach (var target in targets)
        {
            var result = _space.IntersectRay(MapToWorld(point), MapToWorld(target), _excludes);
            // see if there's anything there then add it to the list of valid points
            if (result.Count == 0) validTargets.Add(target);
        }

        return validTargets;
    }

    public List<Vector2> ReconstructPath(Vector2 current)
    {
        var pathStack = new Stack<Vector2>();
        pathStack.Push(MapToWorld(current));

        // get the Vector from where the current point came from and add it to the list
        while (_cameFrom.ContainsKey(current))
        {
            current = _cameFrom[current];
            pathStack.Push(MapToWorld(current));
            //totalPath.Add(_tileMap.MapToWorld(current));
        }

        return pathStack.ToList();
    }

    public List<Vector2> Path(Vector2 start, Vector2 end, Array excludedBodies)
    {
        var iteration = 0;

        _excludes = excludedBodies;

        start = _tileMap.WorldToMap(start);
        end = _tileMap.WorldToMap(end);

        _cameFrom = new System.Collections.Generic.Dictionary<Vector2, Vector2>();
        _gScore = new System.Collections.Generic.Dictionary<Vector2, int>();
        _fScore = new System.Collections.Generic.Dictionary<Vector2, int>();
        _openSet = new List<Vector2>
        {
            start
        };

        _gScore[start] = 0;
        _fScore[start] = (int) end.DistanceTo(start);

        // Visit each point while open set has data and haven't reached max iterations
        while (_openSet.Count > 0 && iteration < _maxIterations)
        {
            var current = FindSmallestFScore();

            // goal was reached so send back the reconstructed path
            if (current == end)
                return ReconstructPath(current);

            _openSet.Remove(current);

            var neighbours = GetNeighbours(current);
            foreach (var neighbour in neighbours)
            {
                var neighbourGScore = int.MaxValue;

                if (_gScore.ContainsKey(neighbour))
                    neighbourGScore = _gScore[neighbour];

                var tentativeGScore = _gScore[current];

                if (tentativeGScore < neighbourGScore)
                {
                    _cameFrom[neighbour] = current;
                    _gScore[neighbour] = tentativeGScore;

                    _fScore[neighbour] = tentativeGScore + (int) end.DistanceTo(neighbour);

                    if (!_openSet.Contains(neighbour))
                        _openSet.Add(neighbour);
                }
            }

            iteration++;
        }

        // else we couldn't find a path
        return null;
    }

    private Vector2 MapToWorld(Vector2 v)
    {
        return _tileMap.MapToWorld(v) + _tileMap.CellSize / 2;
    }
}
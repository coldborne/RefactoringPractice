using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _map;
    [SerializeField, Min(0.001f)] private float _minToInteractionDistance;

    private Transform[] _places;
    private int _currentPlaceIndex;

    private void Update()
    {
        Transform target = _places[_currentPlaceIndex];

        float perStepDistance = _speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, perStepDistance);

        float toTargetSqrDistance = (target.position - transform.position).sqrMagnitude;

        if (toTargetSqrDistance < _minToInteractionDistance * _minToInteractionDistance)
            ChangePlace();
    }

    [ContextMenu("Reserve places from map")]
    private void GetPlaces()
    {
        int placesCount = _map.childCount;
        _places = new Transform[placesCount];

        for (int index = 0; index < placesCount; index++)
            _places[index] = _map.GetChild(index);
    }

    private void ChangePlace()
    {
        _currentPlaceIndex = ++_currentPlaceIndex % _places.Length;

        Vector3 placePosition = _places[_currentPlaceIndex].transform.position;
        Vector3 targetDirection = placePosition - transform.position;

        transform.forward = targetDirection;
    }
}
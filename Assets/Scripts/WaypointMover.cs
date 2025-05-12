using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _map;

    private Transform[] _places;
    private int _currentPlaceIndex;

    private void Start()
    {
        int placesCount = _map.childCount;
        _places = new Transform[placesCount];

        for (int index = 0; index < _map.childCount; index++)
            _places[index] = _map.GetChild(index);
    }

    private void Update()
    {
        Transform target = _places[_currentPlaceIndex];

        float perStepDistance = _speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, perStepDistance);

        if (transform.position == target.position)
            MoveToNextPlace();
    }

    private void MoveToNextPlace()
    {
        _currentPlaceIndex = (_currentPlaceIndex + 1) % _places.Length;

        Vector3 placePosition = _places[_currentPlaceIndex].transform.position;
        Vector3 targetDirection = placePosition - transform.position;

        transform.forward = targetDirection;
    }
}
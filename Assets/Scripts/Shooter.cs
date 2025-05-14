using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed;
    [SerializeField] private float _delay;

    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(KeepShooting());
    }

    private IEnumerator KeepShooting()
    {
        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;

            TakeShot(direction);

            yield return _waitForSeconds;
        }
    }

    private void TakeShot(Vector3 direction)
    {
        Vector3 position = transform.position + direction;

        Rigidbody bullet = Instantiate(_bulletPrefab, position, Quaternion.identity);

        bullet.transform.up = direction;
        bullet.velocity = direction * _speed;
    }
}
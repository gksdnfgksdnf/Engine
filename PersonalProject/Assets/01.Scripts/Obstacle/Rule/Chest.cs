using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator _open;
    [SerializeField] private GameObject _target;
    [SerializeField] private LayerMask _whatIsObstacle;
    [SerializeField] private float _range = 1;
    [SerializeField] private float fadeDuration = 2.0f; // 서서히 사라지는 시간

    private void Start()
    {
        _open = GetComponentInChildren<Animator>();
        _whatIsObstacle = 1 << LayerMask.NameToLayer("Obstacle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            _open.SetTrigger("IsOpen");
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(_target, transform.position, Quaternion.identity);

        }
    }
}

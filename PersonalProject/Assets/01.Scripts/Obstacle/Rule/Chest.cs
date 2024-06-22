using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator _open;
    [SerializeField] private GameObject _target;

    private void Start()
    {
        _open = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.TryGetComponent(out Key key))
        {
            _open.SetTrigger("IsOpen");
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(_target, transform.position, Quaternion.identity);
        }
    }
}

using UnityEngine;

public class Object : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3Int _targetPos;
    private bool _isMoving;
    [SerializeField] private float _objSpeed = 5;
    private Vector3Int _previousPos;
    private Vector3Int _dir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _previousPos = Vector3Int.RoundToInt(_rb.position);
    }

    private void Start()
    {
        _previousPos = Vector3Int.RoundToInt(transform.position);
        GridManager.Instance.SetObject(_previousPos, this);
    }

    public void Interact(Vector3Int dir)
    {
        _dir = dir;
        _targetPos = Vector3Int.RoundToInt(_rb.position + dir);
        if (GridManager.Instance.IsObjectAtPosition(_targetPos))
        {
            Object nextObject = GridManager.Instance.GetObjectAtPosition(_targetPos);
            GridManager.Instance.SetObject(_targetPos, nextObject);
            nextObject.Interact(_dir);
        }
        _isMoving = true;
        // Check if there is another object at the target position
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (CanMoveTo(_dir))
        {
            Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPos, _objSpeed * Time.fixedDeltaTime);

            _rb.MovePosition(newPosition);

            if (Vector3.Distance(_rb.position, _targetPos) <= 0.01f)
            {
                _rb.position = _targetPos;
                GridManager.Instance.SetObject(_targetPos, this, _previousPos);
                _previousPos = _targetPos;
                _isMoving = false;
            }
        }
    }

    private bool CanMoveTo(Vector3Int dir)
    {
        if (GridManager.Instance == null)
        {
            Debug.LogError("GridManager.Instance is null");
            return false;
        }

        Vector3Int targetGridPos = Vector3Int.RoundToInt(_rb.position + dir);

        return targetGridPos.x >= 0 && targetGridPos.x <= GridManager.Instance.x &&
               targetGridPos.y >= 0 && targetGridPos.y <= GridManager.Instance.y &&
               targetGridPos.z >= 0 && targetGridPos.z <= GridManager.Instance.z;
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _dir;
    private Vector3 _targetPos;
    [SerializeField] private Object _targetObject;
    private bool _isMoving = false;
    [SerializeField] private float _playerSpeed = 5f;

    public Vector3 Direction => _dir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GridManager.Instance.SetPlayer(new Vector3Int((int)_rb.position.x, (int)_rb.position.y, (int)_rb.position.z));
    }

    void Update()
    {
        MoveInput();
        MovePlayer();
        ApplyRotation();
    }

    private void MoveInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) SetDirection(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S)) SetDirection(Vector3.back);
        else if (Input.GetKeyDown(KeyCode.A)) SetDirection(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D)) SetDirection(Vector3.right);
    }

    private void MovePlayer()
    {
        if (!_isMoving) return;

        Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPos, _playerSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPos) <= 0.01f)
        {
            _rb.position = _targetPos; // 정확한 위치
            GridManager.Instance.SetPlayer(new Vector3Int((int)_rb.position.x, (int)_rb.position.y, (int)_rb.position.z));
            _isMoving = false;
        }
    }

    private void SetDirection(Vector3 dir)
    {
        if (CanMoveTo(dir))
        {
            if (_isMoving) return;

            _dir = dir;
            _targetPos = _rb.position + _dir;
            Vector3Int targetGridPos = Vector3Int.RoundToInt(_targetPos);

            if (GridManager.Instance.IsObjectAtPosition(targetGridPos))
            {
                _targetObject =  GridManager.Instance.GetObjectAtPosition(targetGridPos);
                _targetObject.Interact();
            }
            else
            {
                _isMoving = true;
            }
        }
    }

    private void ApplyRotation()
    {
        if (_dir.normalized != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_dir.normalized);
    }

    private bool CanMoveTo(Vector3 dir)
    {
        if (GridManager.Instance == null)
        {
            Debug.LogError("GridManager.Instance is null");
            return false;
        }

        Vector3Int targetGridPos = Vector3Int.RoundToInt(_rb.position + dir);

        return targetGridPos.x >= 0 && targetGridPos.x < GridManager.Instance.x &&
               targetGridPos.y >= 0 && targetGridPos.y < GridManager.Instance.y &&
               targetGridPos.z >= 0 && targetGridPos.z < GridManager.Instance.z;// &&
                                                                                //!GridManager.Instance.IsObjectAtPosition(targetGridPos);
    }
}

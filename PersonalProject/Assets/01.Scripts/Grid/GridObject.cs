//using UnityEngine;

//public class GridObject : MonoBehaviour
//{
//    protected Rigidbody rb;
//    protected Vector3Int direction;
//    protected bool isMoving = false;
//    [SerializeField] protected float moveSpeed = 5f;

//    protected virtual void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    protected virtual void Start()
//    {
//        Vector3Int objectPos = GridManager.Instance.WorldToGridPosition(transform.position);
//        GridManager.Instance.AddObject(this, rb.position);
//    }

//    protected virtual void Update()
//    {
//        Move();
//    }

//    protected virtual void Move()
//    {
//        if (!isMoving) return;

//        Vector3 newPosition = transform.position + direction;
//        rb.MovePosition(Vector3.MoveTowards(rb.position, newPosition, moveSpeed * Time.fixedDeltaTime));

//        if (Vector3.Distance(rb.position, newPosition) <= 0.01f)
//        {
//            rb.position = newPosition;
//            GridManager.Instance.MoveObject(this, transform.position, newPosition);
//            isMoving = false;
//        }
//    }

//    public virtual void Interact(Vector3Int dir)
//    {
//        if (!isMoving && GridManager.Instance.CanMoveObject(transform.position, dir))
//        {
//            direction = dir;
//            isMoving = true;
//        }
//    }
//}

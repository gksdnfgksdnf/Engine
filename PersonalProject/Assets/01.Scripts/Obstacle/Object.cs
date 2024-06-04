using UnityEngine;

public class Object : MonoBehaviour, IInteractable
{
    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        GridManager.Instance.SetObject(new Vector3Int((int)_rb.position.x, (int)_rb.position.y, (int)_rb.position.z), this);
    }
    public void Interact()
    {
        Debug.Log("움직여주");
    }
    private void Update()
    {
        
    }
}

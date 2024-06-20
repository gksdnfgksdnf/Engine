using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _range;

    private void Awake()
    {
        _whatIsPlayer = LayerMask.GetMask("Player");
        _range = .9f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IsWinManager.instance.isWin();
        }
    }


    private void OnDrawGizmos()
    {
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale * 0.5f,
                                Vector3.zero, out RaycastHit hit, transform.rotation, _range, _whatIsPlayer);

        if (isHit)
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }

    }
}

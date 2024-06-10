using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    [SerializeField] private bool _isComponentAdded = false;
    private bool _isDestroyComponent;

    public void DrawRay(Vector3 dir, GameObject gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            if (_isComponentAdded) return;
            Debug.Log("You, Push, Win중 하나가 닿음");

            _isDestroyComponent = false;

            if (hit.collider.TryGetComponent(out You you))
            {
                if (you.gameObject.CompareTag("You"))
                {
                    Debug.Log(gameObj.name + "이/가 You가 된다");

                    DestroyAllComponents(gameObj);

                    gameObj.AddComponent<Player>();
                    gameObj.AddComponent<PlayerMovement>();
                }
            }

            if (hit.collider.TryGetComponent(out Push push))
            {
                if (push.gameObject.CompareTag("Push"))
                {
                    Debug.Log(gameObj.name + "은 밀리는 속성을 가진다");
                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Obstacle>();
                }

            }
            if (hit.collider.TryGetComponent(out Win win))
            {
                if (win.gameObject.CompareTag("Win"))
                {
                    Debug.Log(gameObj.name + "은 닿으면 승리한다.");

                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Win>();
                }

            }
            _isComponentAdded = true;
        }
        else
        {
            _isComponentAdded = false;
            DestroyAllComponents(gameObj);
        }
    }

    private void DestroyAllComponents(GameObject obj)
    {
        if (_isDestroyComponent) return;

        Component[] components = obj.GetComponents<Component>();

        foreach (Component component in components)
            if (component is MonoBehaviour)
                Destroy(component);

        _isDestroyComponent = true;
    }
}

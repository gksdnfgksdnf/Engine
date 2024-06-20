using UnityEngine;
using System;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    [SerializeField] private bool _isActioned = false;
    private bool _isDestroyComponent = false; // 초기에는 false로 설정

    public void DrawRay(Vector3 dir, GameObject[] gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            if (_isActioned) return;

            _isDestroyComponent = false; // 초기화

            if (hit.collider.TryGetComponent(out You you))
            {
                if (you.gameObject.CompareTag("You"))
                {
                    for (int i = 0; i < gameObj.Length; i++)
                    {
                        DestroyAllComponents(gameObj[i]);

                        gameObj[i].AddComponent<Player>();
                        gameObj[i].AddComponent<Movement>();
                    }
                }
            }

            if (hit.collider.TryGetComponent(out Push push))
            {
                for (int i = 0; i < gameObj.Length; i++)
                {
                    if (push.gameObject.CompareTag("Push"))
                    {
                        DestroyAllComponents(gameObj[i]);
                        gameObj[i].AddComponent<Obstacle>();
                    }
                }
            }

            if (hit.collider.TryGetComponent(out Win win))
            {
                for (int i = 0; i < gameObj.Length; i++)
                {
                    if (win.gameObject.CompareTag("Win"))
                    {
                        DestroyAllComponents(gameObj[i]);
                        gameObj[i].AddComponent<Win>();
                    }
                }
            }

            if (hit.collider.TryGetComponent(out Subject s))
            {
                for (int i = 0; i < gameObj.Length; i++)
                {
                    if (hit.collider.gameObject.CompareTag("Flag"))
                    {
                        DestroyAllComponents(gameObj[i]);
                        gameObj[i].AddComponent<Win>();
                    }

                    if (hit.collider.gameObject.CompareTag("Baba"))
                    {
                        DestroyAllComponents(gameObj[i]);
                        gameObj[i].AddComponent<Player>();
                        gameObj[i].AddComponent<Movement>();
                    }

                    if (hit.collider.gameObject.CompareTag("Rock"))
                    {
                        DestroyAllComponents(gameObj[i]);

                        Component[] components = hit.collider.GetComponents<Component>();

                        foreach (Component component in components)
                        {
                            if (component is MonoBehaviour)
                            {
                                Type componentType = component.GetType();
                                gameObj[i].AddComponent(componentType);
                            }
                        }
                    }
                }
            }

            _isActioned = true;
        }
        else
        {
            _isActioned = false;
            if (!_isDestroyComponent) // 한 번만 실행되도록 제어
            {
                for (int i = 0; i < gameObj.Length; i++)
                    DestroyAllComponents(gameObj[i]);

                _isDestroyComponent = true; // 파괴 작업이 한 번 완료되었음을 표시
            }
        }
    }

    private void DestroyAllComponents(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();

        foreach (Component component in components)
        {
            if (component is MonoBehaviour)
            {
                Destroy(component);
            }
        }
    }
}

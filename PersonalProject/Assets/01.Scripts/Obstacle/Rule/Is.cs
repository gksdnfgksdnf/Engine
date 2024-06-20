using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    [SerializeField] private bool _isActioned = false;
    private bool _isDestroyComponent;

    public void DrawRay(Vector3 dir, GameObject gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            if (_isActioned) return;

            _isDestroyComponent = false;

            if (hit.collider.TryGetComponent(out You you))
            {
                if (you.gameObject.CompareTag("You"))
                {
                    DestroyAllComponents(gameObj);

                    gameObj.AddComponent<Player>();
                    gameObj.AddComponent<Movement>();
                }
            }

            if (hit.collider.TryGetComponent(out Push push))
            {
                if (push.gameObject.CompareTag("Push"))
                {
                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Obstacle>();
                }

            }

            if (hit.collider.TryGetComponent(out Win win))
            {
                if (win.gameObject.CompareTag("Win"))
                {
                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Win>();
                }

            }

            if (hit.collider.TryGetComponent(out Subject s))
            {
                if(hit.collider.gameObject.CompareTag("Flag"))
                {
                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Win>();
                }

                if(hit.collider.gameObject.CompareTag("Baba"))
                {
                    DestroyAllComponents(gameObj);
                    gameObj.AddComponent<Player>();
                    gameObj.AddComponent<Movement>();
                }

                if(hit.collider.gameObject.CompareTag("Rock"))
                {
                    DestroyAllComponents(gameObj);
                    

                    Component[] components = hit.collider.GetComponents<Component>();
                    
                    foreach(Component component in components)
                    {
                        if (component is MonoBehaviour)
                        {
                            Type componentType = component.GetType();
                            gameObj.AddComponent(componentType);
                        }

                    }
                }
            }


            _isActioned = true;
        }
        else
        {
            _isActioned = false;
            _isDestroyComponent = false;
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

using UnityEngine;
using System;
using System.Collections.Generic;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    private bool _isActioned = false;
    private bool _isDestroyComponent = false;   

    [SerializeField] private List<Type> addedComponents = new List<Type>(); // 추가된 컴포넌트를 추적하기 위한 리스트
        
    public void DrawRay(Vector3 dir, GameObject[] gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            if (_isActioned) return;

            _isDestroyComponent = false;

            //속성 추가
            if (hit.collider.TryGetComponent(out You you))
                addedComponents.Add(typeof(Movement));
            if (hit.collider.TryGetComponent(out Push push))
                addedComponents.Add(typeof(Obstacle));
            if (hit.collider.TryGetComponent(out Win win))
                addedComponents.Add(typeof(Win));
            if (hit.collider.TryGetComponent(out Death death))
                addedComponents.Add(typeof(Skul));
            if (hit.collider.TryGetComponent(out Open open))
                addedComponents.Add(typeof(Key));
            if (hit.collider.TryGetComponent(out Stop stop))
                addedComponents.Add(typeof(Wall));
            if (hit.collider.TryGetComponent(out Lock l))
                addedComponents.Add(typeof(Chest));

            if (hit.collider.TryGetComponent(out Subject s))
            {
                if (hit.collider.gameObject.CompareTag("Flag"))
                    addedComponents.Add(typeof(Win));
                if (hit.collider.gameObject.CompareTag("Baba"))
                    addedComponents.Add(typeof(Movement));
                if (hit.collider.gameObject.CompareTag("Rock"))
                    addedComponents.Add(typeof(Obstacle));
            }

            foreach (GameObject obj in gameObj)
            {
                DestroyComponent(obj);
                foreach (Type componentType in addedComponents)
                {
                    Debug.Log($"{componentType} 추가");
                    obj.AddComponent(componentType);
                }
            }

            _isActioned = true;
        }
        else
        {
            _isActioned = false;

            if(!_isDestroyComponent)
            {
                foreach (GameObject obj in gameObj)
                {
                    DestroyComponent(obj);
                    Debug.Log(obj + "삭제");
                }

                addedComponents.Clear();
                _isDestroyComponent = true;
            }
        }
    }

    public void DestroyComponent(GameObject obj)
    {
        for (int i = addedComponents.Count - 1; i >= 0; i--)
        {
            Type componentType = addedComponents[i];
            MonoBehaviour component = obj.GetComponent(componentType) as MonoBehaviour;
            if (component != null)
            {
                Debug.Log($"{componentType} 제거");
                Destroy(component);
            }
        }
    }
}
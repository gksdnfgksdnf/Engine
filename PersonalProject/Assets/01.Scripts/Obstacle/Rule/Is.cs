using UnityEngine;
using System;
using System.Collections.Generic;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    private List<Type> addedComponents = new List<Type>(); // 추가된 컴포넌트를 추적하기 위한 리스트

    public void DrawRay(Vector3 dir, GameObject[] gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            // 기존에 작업을 수행했으면 바로 리턴
            if (addedComponents.Count > 0)
                return;

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

            if (hit.collider.TryGetComponent(out Subject s))
            {
                if (hit.collider.gameObject.CompareTag("Flag"))
                    addedComponents.Add(typeof(Win));
                else if (hit.collider.gameObject.CompareTag("Baba"))
                    addedComponents.Add(typeof(Movement));
                else if (hit.collider.gameObject.CompareTag("Rock"))
                    addedComponents.Add(typeof(Obstacle));
            }

            // gameObj에 모든 추가된 컴포넌트를 추가
            foreach (GameObject obj in gameObj)
            {
                DestroyComponent(obj); // 기존 컴포넌트 모두 삭제

                foreach (Type componentType in addedComponents)
                {
                    obj.AddComponent(componentType);
                }
            }
        }
        else
        {
            // 아무 것도 닿지 않았을 때 기존 컴포넌트 모두 삭제
            foreach (GameObject obj in gameObj)
            {
                DestroyComponent(obj);
            }

            addedComponents.Clear(); // 추가된 컴포넌트 리스트 초기화
        }
    }

    private void DestroyComponent(GameObject obj)
    {
        foreach (Type componentType in addedComponents)
        {
            MonoBehaviour component = obj.GetComponent(componentType) as MonoBehaviour;
            if (component != null)
            {
                Destroy(component);
            }
        }
    }
}

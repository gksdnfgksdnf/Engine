using UnityEngine;
using System;
using System.Collections.Generic;

public class Is : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private int _range;

    private List<Type> addedComponents = new List<Type>(); // �߰��� ������Ʈ�� �����ϱ� ���� ����Ʈ

    public void DrawRay(Vector3 dir, GameObject[] gameObj)
    {
        bool isHit = Physics.Raycast(transform.position, dir, out RaycastHit hit, _range, _whatisTarget);

        if (isHit)
        {
            // ������ �۾��� ���������� �ٷ� ����
            if (addedComponents.Count > 0)
                return;

            //�Ӽ� �߰�
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

            // gameObj�� ��� �߰��� ������Ʈ�� �߰�
            foreach (GameObject obj in gameObj)
            {
                DestroyComponent(obj); // ���� ������Ʈ ��� ����

                foreach (Type componentType in addedComponents)
                {
                    obj.AddComponent(componentType);
                }
            }
        }
        else
        {
            // �ƹ� �͵� ���� �ʾ��� �� ���� ������Ʈ ��� ����
            foreach (GameObject obj in gameObj)
            {
                DestroyComponent(obj);
            }

            addedComponents.Clear(); // �߰��� ������Ʈ ����Ʈ �ʱ�ȭ
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

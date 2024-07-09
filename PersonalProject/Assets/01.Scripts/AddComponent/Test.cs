using UnityEngine;

public class Test : TestComponent
{
    private TestComponent _testCompo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_testCompo == null)
            {
                gameObject.AddComponent<TestComponent>();
                _testCompo = GetComponent<TestComponent>();
            }
            Debug.Log(a);
        }
    }
}

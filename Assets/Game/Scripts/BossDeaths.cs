using UnityEngine;

public class DestroyOtherObject : MonoBehaviour
{
    public GameObject objectToDestroy; // ������ �� ������, ������� ����� ����������

    private void OnDestroy()
    {
        if (objectToDestroy != null)
        {
            objectToDestroy.SetActive(false);
        }
    }
}
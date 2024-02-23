using UnityEngine;

public class ObjTransform : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = target.position;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // 드랍할 아이템(수정필요)
    [SerializeField] private GameObject _DropObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
        }
    }

    public void Drop()
    {
        GameObject dropObject = Instantiate(_DropObject);

        dropObject.transform.position = Vector3.zero;

        Rigidbody rg = dropObject.GetComponent<Rigidbody>();

        rg.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
    
    }
}

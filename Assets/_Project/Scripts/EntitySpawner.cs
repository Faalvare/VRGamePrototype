using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;

    public void SpawnObject()
    {
        var obj = Instantiate(objectToSpawn, transform);
        obj.transform.localPosition = Vector3.zero;
    }
}

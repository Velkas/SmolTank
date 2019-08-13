using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPyramid : MonoBehaviour
{
    public GameObject prefab;

    private Transform origin;

    // Start is called before the first frame update
    void Start()
    {
        //origin = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("r"))
        // {
        //     Instantiate(prefab, origin.position, origin.rotation);

        //     gameObject.SetActive(false);
        //     Destroy(gameObject);
        // }
    }
}

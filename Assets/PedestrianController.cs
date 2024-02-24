using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnAround());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.16f, transform.position.z);
    }

    IEnumerator TurnAround()
    {
        yield return new WaitForSeconds(30f); // 30 second delay
        transform.Rotate(0f, 180f + transform.rotation.y, 0f); // turn object around
        StartCoroutine(TurnAround());
    }
}

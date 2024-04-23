using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] GameObject containerSensor;
    public bool isBlinking = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkContainerSensor());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BlinkContainerSensor()
    {
        isBlinking = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            containerSensor.SetActive(false);
            yield return new WaitForSeconds(1);
            containerSensor.SetActive(true);
        }

    }
}

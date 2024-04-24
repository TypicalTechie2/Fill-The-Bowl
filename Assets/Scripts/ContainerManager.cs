using System.Collections;
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

    //Set Active and false of the container sensor object to make it visually appear as blinking.
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

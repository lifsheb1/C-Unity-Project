using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrash : MonoBehaviour
{
    public GameObject battery;
    public GameObject waterBottle;
    public GameObject glassBottle;
    public GameObject plasticCup;
    public GameObject toiletPaper;
    public GameObject cerealBox;

    public static GenerateTrash Instance;
    private int xPos; // -30 to 50
    private int zPos; // -40 to 720
    private int objectToGenerate;

    private int objectQuantity;
    // Start is called before the first frame update

    public void spawnObjects(int amount)
    {
        StartCoroutine(GenerateObjects(amount));
    }

    private void Awake()
    {
        objectQuantity = 0;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateObjects(int amount)
    {
        objectQuantity = 0;
        while (objectQuantity < amount)
        {
            objectToGenerate = Random.Range(1, 7);
            xPos = Random.Range(-30, 51);
            zPos = Random.Range(-40, 895);
            if ((zPos >= 366f && zPos <= 400f) || (zPos >= 865f && zPos <= 895f))
            {
                xPos = Random.Range(-30, 460);
            }

            switch (objectToGenerate)
            {
                case 1:
                    Instantiate(battery, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(waterBottle, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(glassBottle, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
                case 4:
                    Instantiate(plasticCup, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
                case 5:
                    Instantiate(toiletPaper, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
                case 6:
                    Instantiate(cerealBox, new Vector3(xPos, 0.23f, zPos), Quaternion.identity);
                    break;
            }

            objectQuantity += 1;
        }

        yield return new WaitForSeconds(1);
    }
}
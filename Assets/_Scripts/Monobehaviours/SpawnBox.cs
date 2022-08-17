using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public GameObject[] boxes;

    Vector3 spawnPosition;
    Vector3 dropPosition;

    GameObject spawnBoxPrefab;

    public GameObject currentBox;

    Box boxData;


    public void CreateBox()
    {
        spawnBoxPrefab = GetRandomBox();
        spawnPosition = GetRandomSpawnPosition();

        currentBox = Instantiate(spawnBoxPrefab, spawnPosition, Quaternion.identity);
        currentBox.transform.parent = GameObject.Find("Board").transform;

        dropPosition = GetRandomDropPosition();

        boxData = currentBox.GetComponent<BoxData>().boxData;

    }

    public IEnumerator MoveToDropPosition()
    {
       while(currentBox.transform.position != dropPosition)
       {
            Vector3 newPos = Vector3.MoveTowards(currentBox.transform.position, dropPosition, 50f * Time.deltaTime);
            currentBox.transform.position = newPos;
            yield return new WaitForEndOfFrame();
       }
    }

    Vector3 GetRandomSpawnPosition()
    {
        return spawnPoints[Random.Range(0, 2)].transform.position;
    }

    Vector3 GetRandomDropPosition()
    {
        return new Vector3(Random.Range(0, 12), 0);
    }

    GameObject GetRandomBox()
    {
        return boxes[Random.Range(0, 7)];
    }
}

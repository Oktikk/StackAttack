using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpawnBox spawnBox;


    private void Start()
    {
        spawnBox = GetComponent<SpawnBox>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            if (spawnBox.currentBox != null)
            {
                Destroy(spawnBox.currentBox);
            }
            spawnBox.CreateBox();
            StartCoroutine(spawnBox.MoveToDropPosition());
        }
    }
}

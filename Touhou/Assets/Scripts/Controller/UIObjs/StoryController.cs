using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] storyCut = new GameObject[12];

    private int cutNumber = default;



    


    // Start is called before the first frame update
    void Start()
    {
        cutNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

        storyCut[cutNumber].SetActive(true);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (cutNumber < 11)
            {
                storyCut[cutNumber].SetActive(false);
                cutNumber++;
            }
            if (cutNumber == 11)
            {
                Time.timeScale = 1.0f;
                gameObject.SetActive(false);
            }
        }
    }
}

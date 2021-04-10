using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    public static PlayerHearts instance;
    [SerializeField] GameObject heartContainer;
    [SerializeField] List<GameObject> heartContainerList;

    [HideInInspector]
    public int totalHearts;
    [HideInInspector]
    public float currentHearts;
    HearthContainer currentContainer;

    [Range(0, 1)] float fill;
    [SerializeField] Image fillImage;

    private void Start()
    {
        instance = this;
        heartContainerList = new List<GameObject>();
    }

    public void SetupHearts(int heartsIn)
    {
        heartContainerList.Clear();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalHearts = heartsIn;
        currentHearts = (float)totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainer, transform);
            heartContainerList.Add(newHeart);

            if (currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HearthContainer>();
            }
            currentContainer = newHeart.GetComponent<HearthContainer>();
        }
        currentContainer = heartContainerList[0].GetComponent<HearthContainer>();
    }

    public void SetCurrentHealth(float health)
    {
        currentHearts = health;
        currentContainer.SetHearts(currentHearts);
    }

    public void AddHearts(float healthUp)
    {
        currentHearts += healthUp;
        if (currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }
        currentContainer.SetHearts(currentHearts);
    }

    public void RemoveHearts(float healthDown)
    {
        currentHearts -= healthDown;
        if (currentHearts < 0)
        {
            currentHearts = 0f;
        }
        currentContainer.SetHearts(currentHearts);
    }

    public void AddContainer()
    {
        GameObject newHeart = Instantiate(heartContainer, transform);
        currentContainer = heartContainerList[heartContainerList.Count - 1].GetComponent<HearthContainer>();
        heartContainerList.Add(newHeart);

        if (currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HearthContainer>();
        }
        currentContainer = heartContainerList[0].GetComponent<HearthContainer>();
        totalHearts++;
        currentHearts = totalHearts;
        SetCurrentHealth(currentHearts);
    }
}
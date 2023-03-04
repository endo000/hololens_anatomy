using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanLayerController : MonoBehaviour
{
    private List<Transform> children = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            children.Add(child);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetActiveChild(GameObject activeChild)
    {
        foreach (var child in children)
        {
            child.gameObject.SetActive(child.gameObject.Equals(activeChild));
        }
    }
}
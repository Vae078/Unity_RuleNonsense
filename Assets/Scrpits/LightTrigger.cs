using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LightTrigger : MonoBehaviour
{
    
    List<TextMeshPro> texts = new List<TextMeshPro>();
    private void OnTriggerStay(Collider other)
    {
       if (other.CompareTag("text"))
        {
            TextMeshPro textCompent = other.GetComponent<TextMeshPro>();
            if (textCompent != null && !texts.Contains(textCompent))
            {
                texts.Add(textCompent);
                Debug.Log("Add yes");
                textCompent.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("text"))
        {
            TextMeshPro textCompent = other.GetComponent<TextMeshPro>();
            if (textCompent != null && texts.Contains(textCompent))
            {
                texts.Remove(textCompent);
                Debug.Log($"Remove text:{other.name}");
                textCompent.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }


    private void Update()
    {
        foreach(TextMeshPro i in texts)
        {
            Debug.Log(i);
        }
    }
    




}

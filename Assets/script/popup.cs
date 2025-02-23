using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public string value;

    private void Start()
    {
        text.text = value;
        Destroy(gameObject, 1f);
    }

}

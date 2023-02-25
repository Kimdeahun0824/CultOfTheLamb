using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler
{
    public bool IsTouched;

    public void OnPointerClick(PointerEventData eventData)
    {
        IsTouched = true;
        Debug.Log($"test");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsTouched = true;
        Debug.Log($"test");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsTouched = true;
        Debug.Log($"test");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouched) return;
    }
}

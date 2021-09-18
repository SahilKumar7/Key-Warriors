using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    // display panel
    public virtual void Show(){
        Debug.Log("hello");
        gameObject.SetActive(true);
    }

    // hide panel

    public virtual void Hide(){
        gameObject.SetActive(false);
    }
}

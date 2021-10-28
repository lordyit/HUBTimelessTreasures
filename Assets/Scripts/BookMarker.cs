using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMarker : MonoBehaviour
{
    [SerializeField]
    private Book book = null;
    [SerializeField]
    private int markerPage = 0;

    public void GoToMarkedPage() 
    {
        book.GoToPage(markerPage);    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDiscription : MonoBehaviour
{
    public GameController gameManager;
    public GameObject enterBtn;
    public string pieceID;
    
    void Start()
    {
        pieceID = gameObject.name; 
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player"){
            gameManager.pieceID = pieceID;
            enterBtn.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        enterBtn.SetActive(false);    
        gameManager.pieceID = null;
    }
}

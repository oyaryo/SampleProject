using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperManager : MonoBehaviour
{
    public GameController gameManager;
    public Popup_Information popup_Information;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(gameManager.ticketFlag == true){
            this.gameObject.SetActive(false);
            return;
        }
        popup_Information.Popup_Open();
    }
}

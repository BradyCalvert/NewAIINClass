using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public int playerHealth;
    public GameObject player;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        player = GameObject.FindWithTag("Player");



        }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
	}
    void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if(playerHealth<=0)
        {
            Debug.Log("PLAYER DONE DID DIE");
        }
    }
}



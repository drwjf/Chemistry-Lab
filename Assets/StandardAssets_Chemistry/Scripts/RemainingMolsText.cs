using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingMolsText : MonoBehaviour {

    public int type; // type = 1 means molecule  naming || type = 2 means construction.

    // Use this for initialization
    void Start()
    {
        if (type == 1)
        {
			gameObject.GetComponent<Text>().text = (MoleculeManager.instance.strategy.Count - GameManager.namedMolecules).ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text = (MoleculeManager.instance.strategy.Count - GameManager.constructedMolecules).ToString();
        }
    }
}

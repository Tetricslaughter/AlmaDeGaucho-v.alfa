using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    private int amountFac;
    public Text amountFacText;
    private int amountBol;
    public Text amountBolText;
    // Start is called before the first frame update
    void Start()
    {
        amountFac = 0;
        amountBol = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Detecto la colision");

         switch (other.gameObject.tag)
         {
            case "Facon":
                  amountFac += 1;
                  amountFacText.text = "x " + amountFac.ToString();
                  Destroy(other.gameObject);
                  break;

            case "Boleadora":
                 amountBol += 1;
                 amountBolText.text = "x " + amountBol.ToString();
                 Destroy(other.gameObject);
                 break;

            default:
                 Debug.Log("No se reconoce objeto");
                 break;
         }
    }
}

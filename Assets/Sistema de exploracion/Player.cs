using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Horse currentHorse;
    private bool canMount = false;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMount && Input.GetKeyDown(KeyCode.E))
        {
            MountHorse();
           
        }
        else if (currentHorse != null && Input.GetKeyDown(KeyCode.Q))
        {
            DismountHorse();
        }
        
    }

    public void CanMount(Horse horse)
    {
        currentHorse = horse;
        canMount = horse != null;
    }

    private void MountHorse()
    {
        if (currentHorse != null)
        {
            transform.position = currentHorse.saddlePosition.position;
            transform.SetParent(currentHorse.transform);
            //transform.localScale = originalScale; // Asegura que la escala se mantenga
            playerMovement.enabled = false;//  Desactivar el control del jugador
            currentHorse.GetComponent<HorseMovement>().enabled = true; // Activar el control del caballo
            
        }
        
    }

    private void DismountHorse()
    {
        if (currentHorse != null)
        {
            transform.SetParent(null);
            //transform.localScale = originalScale; // Asegura que la escala se mantenga
            playerMovement.enabled = true;//  Reactivar el control del jugador
            currentHorse.GetComponent<HorseMovement>().enabled = false; // Desactivar el control del caballo
            currentHorse = null;
        }
    }
}

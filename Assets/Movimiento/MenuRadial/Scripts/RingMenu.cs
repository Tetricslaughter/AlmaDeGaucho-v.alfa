﻿using SUPERCharacte;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public RingArmas[] ringBools;
    public RingPiece ringPiecePrefab;
    public ArmaPlayer script;
    //public Animator playerAnimator;

    private RingPiece[] ringPieces;
    private float degreesPerPiece;
    private float gapDegrees = 2f;

    private void Start()
    {
        degreesPerPiece = 360f / ringBools.Length;

        float distanceToIcon = Vector3.Distance(ringPiecePrefab.icon.transform.position, ringPiecePrefab.background.transform.position);

        ringPieces = new RingPiece[ringBools.Length];

        for (int i = 0; i < ringBools.Length; i++) 
        {
            ringPieces[i] = Instantiate(ringPiecePrefab, transform);

            ringPieces[i].background.fillAmount = (1f / ringBools.Length) - (gapDegrees / 360f);
            ringPieces[i].background.transform.localRotation = Quaternion.Euler(0, 0, degreesPerPiece / 2f + gapDegrees / 2f + i * degreesPerPiece);

            ringPieces[i].icon.sprite = ringBools[i].icon;

            Vector3 directionVector = Quaternion.AngleAxis(i * degreesPerPiece, Vector3.forward) * Vector3.up;
            Vector3 movementVector = directionVector * distanceToIcon;
            ringPieces[i].icon.transform.localPosition = ringPieces[i].background.transform.localPosition + movementVector;
        }
    }

    private void Update()
    {
        int activeElement = GetActiveElement();
        HighlightActiveElement(activeElement);
        RespondToMouseInput(activeElement);
    }

    private int GetActiveElement() 
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
        Vector3 cursorVector = Input.mousePosition - screenCenter;

        float mouseAngle = Vector3.SignedAngle(Vector3.up, cursorVector, Vector3.forward) + degreesPerPiece / 2f;
        float normalizedMouseAngle = NormalizeAngle(mouseAngle);

        return (int)(normalizedMouseAngle / degreesPerPiece);
    }

    private void HighlightActiveElement(int activeElement)
    {
        for (int i = 0; i < ringPieces.Length; i++) 
        { 
            if(i == activeElement)
            {
                ringPieces[i].background.color = new Color(1f, 1f, 1f, 0.75f);
            } 
            else
            {
                ringPieces[i].background.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }

    private void RespondToMouseInput(int activeElement)
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ringBools[activeElement].manos)
            {
                script.pressManos=true;
            }else if (ringBools[activeElement].faconYPoncho)
            {
                script.pressEquiparFacon=true;
            }else if (ringBools[activeElement].rifle)
            {
                script.pressRifle=true;
            }else if (ringBools[activeElement].boleadoras)
            {
                script.pressBoleadoras=true;
            }
            //playerAnimator.SetTrigger(ringAnimations[activeElement].name);
            gameObject.SetActive(false);
        }
    }

    // Función para normalizar el ángulo
    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}

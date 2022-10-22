using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ---------------------

    [SerializeField] Transform cannon;
    [SerializeField] Vector2 sensibilidad;  // Marca la Sensibilidad del movimiento
    //[SerializeField] Transform Camera;      // La posición de una cámara que seguirá el movimiento en Y

    //---------------------- PROPIEDADES PRIVADAS ---------------------

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotacionPlayer();
    }

    private void RotacionPlayer()
    {
        float horizontal = Input.GetAxis("Mouse X");
        //float vertical = Input.GetAxis("Mouse Y");

        if (horizontal != 0)
        {
            cannon.Rotate(0, horizontal * sensibilidad.x, 0);
        }

        //Para movimiento vertical de la cámara
        /*
        if (vertical != 0)
        {
            Vector3 rotation = Camera.localEulerAngles;
            rotation.x = (rotation.x - vertical * sensibilidad.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; }
            else if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

            Camera.localEulerAngles = rotation;
        }
        */
    }
}

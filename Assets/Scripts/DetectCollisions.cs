using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    /*Variables de las particulas
    public ParticleSystem particulas;
    public ParticleSystem wall;
    public ParticleSystem cofres;
    public ParticleSystem player;*/

    

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (gameObject.CompareTag("Proyectil") && otherCollider.gameObject.CompareTag("ground"))
        {
            //Si la bala colisiona con un enemigo se destruyen ambos
            Destroy(gameObject);//Bala
           

        }
       
    }  
}

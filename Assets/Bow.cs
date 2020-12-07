using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
  [Header("Serialize Fields")]
  [SerializeField] private GameObject arrow = null;
  [SerializeField] private Transform shootPoint = null;

  [Header("Configurables")]
  public float force;

  void Update()
  {
    Vector2 bowPosition = transform.position;
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mousePosition - bowPosition;
    transform.right = direction;
  }

  public void Shoot()
  {
    GameObject arrowInstance = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
    arrowInstance.GetComponent<Rigidbody2D>().velocity = transform.right * force;
  }
}

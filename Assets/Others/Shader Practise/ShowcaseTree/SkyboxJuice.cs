using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxJuice : MonoBehaviour
{
    Skybox s;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Skybox>();
    }

    // Update is called once per frame
    void Update()
    {
        s.material.SetFloat("_Rotation", Time.time * speed);
    }
}

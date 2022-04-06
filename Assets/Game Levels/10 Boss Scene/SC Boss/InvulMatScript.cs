using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulMatScript : MonoBehaviour
{
    [SerializeField] Material mat;
    EnemyStatManager esm;
    [SerializeField] float b;
    private void Start()
    {
        esm = GetComponentInParent<EnemyStatManager>();
        SetMaterialValue(0);
    }

    public void SetMaterialValue(float a)
    {
        if (a > 0.1f)
        {
            esm.invulnerable = true;
        }
        else
        {
            esm.invulnerable = false;
        }
        mat.SetFloat("_ControlMaterial", a);
    }
}

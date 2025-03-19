using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ContorlIK : MonoBehaviour
{
    public TwoBoneIKConstraint twoBoneIk;
    public static ContorlIK instance;
    [SerializeField]private Transform contorlTransform;
    [SerializeField]public Transform objectTranform;
    [SerializeField] private RigBuilder rigBuilder;

    public static ContorlIK GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameRoot »ñµÃÊµÀýÊ§°Ü");
            return instance;
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }

        twoBoneIk = GetComponent<TwoBoneIKConstraint>();
        //Holding();
        
    }


    public void Holding()
    {
        Debug.Log("yes im holding");
        twoBoneIk.data.target = contorlTransform;
        rigBuilder.Build();
    }

    public void UnHolding()
    {
        twoBoneIk.data.target = null;
        rigBuilder.Build();

    }




}

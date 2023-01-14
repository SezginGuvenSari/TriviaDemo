using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    #region Serialize

    [SerializeField] private PersonData _personData;

    #endregion

    #region Properties

  
    public PersonData PersonData
    {
        get => _personData;
        set => _personData = value;
    }

    #endregion

    private void Awake() => _personData = GetComponent<PersonData>();
}

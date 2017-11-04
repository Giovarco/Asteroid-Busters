using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour, IHaveStatus {

    public int hp;
    public Status status;

    Status IHaveStatus.status{ get; }

}

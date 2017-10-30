using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour, IHaveStatus {

    // PUBLIC
    public int hp;
    public Status status;

    // PRIVATE
    Status IHaveStatus.status{ get; }

}

using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {

    public GameObject owner;
    public string threatTag;

    void OnTriggerEnter(Collider other) {
        if (other.tag == threatTag) {
            owner.SendMessage("OnVisionEnter", other);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == threatTag) {
            owner.SendMessage("OnVisionExit", other);
        }
    }

}

using UnityEngine;

public class PlanetRotator : MonoBehaviour {
    public float _speed = 1;
	void Update (){
        transform.Rotate(0, 2 * Time.deltaTime * _speed, 0);
	}
}

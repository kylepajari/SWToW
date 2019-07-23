using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitScript : MonoBehaviour {

public Transform sparkEffect;
public Transform electricEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Empire"){
			if(transform.name.Contains("red")){
				Instantiate(sparkEffect, transform.position, transform.rotation);
			}
			if(transform.name.Contains("ion")){
				Instantiate(electricEffect, transform.position, transform.rotation);
			}	
		}
		else if(col.tag == "Rebel"){
			if(transform.name.Contains("green")){
				Instantiate(sparkEffect, transform.position, transform.rotation);
			}
			if(transform.name.Contains("ion")){
				Instantiate(electricEffect, transform.position, transform.rotation);
			}	
		}
		else{
			if(!col.name.Contains("laser")){
				if(transform.name.Contains("green") || transform.name.Contains("red")){
					Instantiate(sparkEffect, transform.position, transform.rotation);
				}
				if(transform.name.Contains("ion")){
					Instantiate(electricEffect, transform.position, transform.rotation);
				}	
			}	
		}
	}
}

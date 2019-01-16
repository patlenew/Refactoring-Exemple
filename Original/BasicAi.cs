using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Character.ThirdPerson
{
	public class BasicAi : MonoBehaviour {


		public enum State {Patrol, Chase};
		public State state;
		public Kraken kraken;
		GameObject preKra;
		bool alive,attacking;
		public bool aiActive;
		public static bool canUnder;
		public GameObject dir;
		int waypointInt;
		public float patrolSpeed = 1, chaseSpeed = 1;
		public GameObject targetAI;
		public GameObject[] waypoints;
		float timerUnderWater = 7;

		// Use this for initialization
		void Start () {
			if (PlayerPrefs.GetInt("KrakenAI") == 1)
				StartCoroutine(waitStartGame());
		}

		// Update is called once per frame
		void Update () {
			if (PlayerPrefs.GetInt("KrakenAI") == 1)
				transform.LookAt (targetAI.transform);

       
		}

		IEnumerator waitStartGame()
		{
			yield return new WaitForSeconds(2.7f);
			state = BasicAi.State.Patrol;
			alive = true;
			preKra = kraken.projectileKra.gameObject;
			StartCoroutine (actions ());
		}

		IEnumerator actions()
		{
			while (alive) {
				switch (state) {
				case State.Patrol:
					Patrol ();
					break;

				case State.Chase:
					Chase ();
					break;
				}
				yield return null;
			}
			
		}

		void Patrol()
		{
			
			if(timerUnderWater <= 0 && !canUnder)
			{
				canUnder = true;
				timerUnderWater = 7;
			}
			else if(!canUnder) 
			timerUnderWater -= Time.deltaTime;


			if (waypointInt >= waypoints.Length) {
				waypointInt = 0;
			
			}

			if (Vector3.Distance (transform.position, targetAI.transform.position) <= 58 && !attacking && !canUnder) {
				
				state = State.Chase;

			}

			if(Vector3.Distance(transform.position, waypoints[waypointInt].transform.position) >= 8 && !canUnder)
			{
				transform.position = Vector3.Lerp (transform.position, waypoints[waypointInt].transform.position , 0.7f * Time.deltaTime);
               

            }
			else if(Vector3.Distance(transform.position, waypoints[waypointInt].transform.position) <= 8 && !canUnder)
			{
				waypointInt+=1;
          
			}


		}

		void Chase()
		{
			if(!attacking)
				StartCoroutine (Shoot());

		}

		IEnumerator Shoot()
		{
			if (!attacking) {
				GameObject g = Instantiate (preKra, new Vector3(transform.position.x,transform.position.y + 5, transform.position.z), Quaternion.identity) as GameObject;
				g.GetComponent<ParticleSystem>().transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
				Physics.IgnoreCollision(GetComponent<Collider>(), g.GetComponent<Collider>());
				g.GetComponent<ProjectileManager>().krakenCol = gameObject;
				g.AddComponent<Rigidbody>();
				g.GetComponent<Rigidbody>().velocity = (dir.transform.position - transform.position).normalized * 100; 
				attacking = true;

			}
			yield return new WaitForSeconds (.1f);
			state = State.Patrol;
			yield return new WaitForSeconds (5f);
			attacking = false;
		}
	}
}

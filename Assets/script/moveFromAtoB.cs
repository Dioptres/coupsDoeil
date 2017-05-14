using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class moveFromAtoB : Lookable {

	Vector3 goThere;
	Vector3 previousLookPoint;
	GazePoint gazePoint;
	UnityEngine.AI.NavMeshAgent agent;
	float i;
	bool doAct;
	bool goingUp;
	bool goingRight;

	public override void QuitSee () {
		doAct = true;
	}

	IEnumerator moveMeThere () {
		yield return new WaitForSeconds (2);

		if (gazePoint.IsValid) {

		}

	}

	protected override void StartLookable () {
		base.StartLookable ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		i = 0;
		doAct = false;
		previousLookPoint = Vector3.zero;
	}

	protected override void UpdateLookable () {
		base.UpdateLookable ();
		gazePoint = EyeTracking.GetGazePoint ();
	}

	private void FixedUpdate () {

		if (doAct) {
			if (previousLookPoint != Vector3.zero) {
				Vector3 now = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
				if ((goingUp && now.x < previousLookPoint.x) || (!goingUp && now.x > previousLookPoint.x) || (goingRight && now.z < previousLookPoint.z) || (!goingRight && now.z > previousLookPoint.z)) {
					goThere = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
					agent.destination = goThere;
					doAct = false;
					previousLookPoint = Vector3.zero;
				}
				previousLookPoint = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
			}
			else {
				previousLookPoint = Camera.main.ScreenToWorldPoint (new Vector3 (gazePoint.Screen.x, gazePoint.Screen.y, 10));
				if (previousLookPoint.x < this.transform.position.x) {
					goingUp = false;
				}
				else {
					goingUp = true;
				}
				if (previousLookPoint.z < this.transform.position.z) {
					goingRight = false;
				}
				else {
					goingRight = true;
				}
			}
		}
	}
}

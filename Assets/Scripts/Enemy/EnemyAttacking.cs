using UnityEngine;
using System.Collections;
using System.Timers;


public abstract class EnemyAttacking : MonoBehaviour
{
	public int damage = 3;                     // How much damage the enemy does.
	public int attackWaitPeriod = 1000;        // How long the enemy will wait between attacks. (in milliseconds)
	bool canAttack = true;
	bool attackOnCooldown = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Attack(){
		if (canAttack && !attackOnCooldown) {
			doAttack ();
			AttackWait ();
		}
	}

	protected abstract void doAttack ();
		

	public void DisableAttacking(){
		canAttack = false;
	}

	/// <summary>
	/// Enables attacking if the attack is off cooldown
	/// </summary>
	public void EnableAttacking(){
		canAttack = true;
	}
		

	public bool canEnemyAttack(){
		return canAttack;
	}


	/// <summary>
	/// After attacking, the slime must wait temporarily before attacking again.
	/// </summary>
	protected void AttackWait()
	{
		attackOnCooldown = true;

		Timer t = new Timer();
		t.Elapsed += new ElapsedEventHandler(AttackWaitFinished);
		t.Interval = attackWaitPeriod;
		t.AutoReset = false;
		t.Start();
	}

	/// <summary>
	/// The slime is able to attack again after the set period of time.
	/// </summary>
	void AttackWaitFinished(object sender, ElapsedEventArgs args)
	{
		attackOnCooldown = false;
		((Timer)sender).Dispose();
	}

}


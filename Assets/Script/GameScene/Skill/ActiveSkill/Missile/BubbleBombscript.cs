using UnityEngine;
using System.Collections;

namespace EpicToonFX
{

	public class BubbleBombscript : MonoBehaviour
	{
	
		public float randomPercent = 10;
	
		void Start ()
		{
        transform.GetComponent<AudioSource>().pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);
		}


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Monster"))
            {
                Monster monster = collision.GetComponent<Monster>();
                monster.TakeDamage((int)((float)(StageManager.Instance.playerScript.damage)*0.7f));
            }
        }
    }
}
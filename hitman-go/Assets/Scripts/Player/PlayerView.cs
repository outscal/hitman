using UnityEngine;
using Enemy;
using System.Threading.Tasks;
using System.Collections;
using Common;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public void DisablePlayer()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

       async public Task MoveToLocation(Vector3 _location)
        {
            this.transform.LookAt(_location);
            // this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, _location, 1f);
            iTween.MoveTo(this.gameObject, _location, 0.5f);
            await new WaitForSeconds(0.5f);
        }

        public void PlayAnimation(PlayerStates state)
        {
           
        }

        public void Reset()
        {
            Destroy(gameObject);
        }
    }
}
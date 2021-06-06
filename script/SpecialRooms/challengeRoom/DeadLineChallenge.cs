using UnityEngine;

public class DeadLineChallenge : MonoBehaviour
{
    public ChallengeRoom challengeRoomConfig;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            challengeRoomConfig.lifePlayer = 0;
        }
    }
}

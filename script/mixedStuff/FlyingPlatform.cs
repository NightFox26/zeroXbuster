using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    public Vector3 minTraj;
    public Vector3 maxTraj;
    private Vector3 initPos;
    private bool goRight = true;
    private bool goUp = true;
    private bool horizMovementAllow = true;
    private bool verticalMovementAllow = true;
    private bool oblicMovementAllow = false;
    public float velocity = 20;

    private void Start() {
        initPos = transform.position;
        if(maxTraj.x == 0 && minTraj.x == 0) horizMovementAllow = false;
        if(maxTraj.y == 0 && minTraj.y == 0) verticalMovementAllow = false;

        if(horizMovementAllow && verticalMovementAllow){
            oblicMovementAllow = true;
        }
    }

    private void Update() {
        if(oblicMovementAllow){
            if(transform.position.x >= initPos.x + maxTraj.x && (transform.position.y >= initPos.y + maxTraj.y || transform.position.y <= initPos.y - maxTraj.y)){
                goRight = false;
            }else if(transform.position.x <= initPos.x + minTraj.x && (transform.position.y <= initPos.y + minTraj.y || transform.position.y >= initPos.y - minTraj.y )) {
                goRight = true;
            }
            return;
        }

        if(horizMovementAllow){
            if(transform.position.x >= initPos.x + maxTraj.x){
                goRight = false;
            }else if(transform.position.x <= initPos.x + minTraj.x) {
                goRight = true;
            }
            return;
        }

        if(verticalMovementAllow){
            if(transform.position.y >= initPos.y + maxTraj.y){
                goUp = false;
            }else if(transform.position.y <= initPos.y + minTraj.y) {
                goUp = true;
            }
            return;
        }
    }

    private void FixedUpdate() {   
        if(oblicMovementAllow){
            if(!goRight){
                transform.Translate(minTraj.normalized*Time.deltaTime*velocity,Space.Self);
            }else if(goRight){
                transform.Translate(maxTraj.normalized*Time.deltaTime*velocity,Space.Self);
            }
            return;
        }

        if(!goRight && horizMovementAllow){
            transform.Translate(Vector3.left*Time.deltaTime*velocity,Space.Self);
        }else if(goRight && horizMovementAllow){
            transform.Translate(Vector3.right*Time.deltaTime*velocity,Space.Self);
        }

        if(!goUp && verticalMovementAllow){
            transform.Translate(Vector3.down*Time.deltaTime*velocity,Space.Self);
        }else if(goUp && verticalMovementAllow){
            transform.Translate(Vector3.up*Time.deltaTime*velocity,Space.Self);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(transform.position + minTraj,transform.position + maxTraj);
    }
}

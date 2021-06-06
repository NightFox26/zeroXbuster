
public class EnemyFailleKeeper : Enemy
{
    protected override void die(bool isCriticalDie = false){        
        base.die(false);
        FailleConfig.instance.exitFaille(true);
    }
}

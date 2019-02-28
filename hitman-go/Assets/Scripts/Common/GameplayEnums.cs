namespace Common
{
    public enum Directions
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public enum GameStatesType
    {
        PLAYERSTATE,
        ENEMYSTATE,
        GAMEOVERSTATE
    }
    public enum EnemyType
    {
        STATIC,
        PATROLLING,
        ROTATING_KNIFE,
        SNIPER,
        SHIELDED,
        DOGS,
        CIRCULAR_COP,
        TARGET
    }
    public enum InteractablePickup
    {
        NONE,
        BREIFCASE,
        STONE,
        BONE,
        SNIPER_GUN,
        DUAL_GUN,
        TRAP_DOOR,
        COLOR_KEY,
        AMBUSH_PLANT,
        GUARD_DISGUISE
    }
    public enum PlayerStates
    {
        IDLE,
        AMBUSH,
        DISGUISE,
        UNLOCK_DOOR,
        SHOOTING,
        WAIT_FOR_INPUT

    }
}
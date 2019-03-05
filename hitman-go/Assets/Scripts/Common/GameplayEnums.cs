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
        GAMEOVERSTATE,
        LEVELFINISHEDSTATE,
        LOBBYSTATE,
        LOADLEVELSTATE
    }
    public enum StarTypes{
        PLAYERMOVES,
        NOKILL,
        ALLKILL,
        PICKBRIEFCASE,
        KILLDOGS,
        NOKILLDOGS
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
     public enum NodeProperty
    {
        NONE,
        SPAWNPLAYER,
        TARGETNODE,
        TELEPORT

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
        THROWING,
        WAIT_FOR_INPUT,
        END_TURN,
        INTERMEDIATE_MOVE,
        NONE

    }
    public enum EnemyStates
    {
        IDLE,
        MOVING,
        CHASE
    }
}
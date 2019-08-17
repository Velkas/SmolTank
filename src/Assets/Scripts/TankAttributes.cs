public class TankAttributes
{
    [System.Serializable]
    public enum Movement
    {
        Stationary = 0,
        Slow = 8,
        Normal = 12,
        Fast = 15
    }

    [System.Serializable]
    public enum Behavior
    {
        Controlled,
        Passive,
        Defensive,
        Active,
        Offensive
    }

    [System.Serializable]
    public enum BulletSpeed
    {
        Normal = 15,
        Fast = 20
    }

    [System.Serializable]
    public enum FireRate
    {
        Slow = 1,
        Fast = 4
    }

    [System.Serializable]
    public enum Ricochets
    {
        None = 0,
        Single = 1,
        Multiple = 2
    }

    [System.Serializable]
    public enum BulletLimit
    {
        None = 0,
        Single = 1,
        Small = 2,
        Medium = 3,
        Large = 5
    }

    [System.Serializable]
    public enum MineLimit
    {
        None = 0,
        Small = 2,
        Large = 4
    }

    public Movement movement;
    public Behavior behavior;
    public BulletSpeed bulletSpeed;
    public FireRate fireRate;
    public Ricochets ricochets;
    public BulletLimit bulletLimit;
    public MineLimit mineLimit;

    public TankAttributes GetTankAttributes(string tankType)
    {
        TankAttributes attributes = new TankAttributes();

        switch (tankType.ToLowerInvariant())
        {
            case "player":
                attributes.movement = TankAttributes.Movement.Normal;
                attributes.behavior = TankAttributes.Behavior.Controlled;
                attributes.bulletSpeed = TankAttributes.BulletSpeed.Normal;
                attributes.fireRate = TankAttributes.FireRate.Fast;
                attributes.ricochets = TankAttributes.Ricochets.Single;
                attributes.bulletLimit = TankAttributes.BulletLimit.Large;
                attributes.mineLimit = TankAttributes.MineLimit.Small;
                break;
            default:
                attributes = null;
                break;
        }

        return attributes;
    }
}

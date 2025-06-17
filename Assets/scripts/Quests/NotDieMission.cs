
public class NotDieMission : Mission
{
    public override bool Check()
    {
        return LiveManager.Instance.deathCount == 0;
    }
}

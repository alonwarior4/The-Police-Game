


public interface ISavable
{
    string pathOffset { get; }
}


[System.Serializable]
public class SaveData
{
    public SaveData (ISavable savable)
    {

    }
}

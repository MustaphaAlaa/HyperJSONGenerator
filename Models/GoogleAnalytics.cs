namespace FileCreator;

public class GoogleAnalytics
{
    public DateTime Date { get; set; }
    public string Page { get; set; }
    public int Users { get; set; }
    public int Views { get; set; }
    public int Sessions { get; set; }

    public override string ToString()
    {
        return $"{Date} \n";
    }
}
namespace Metriflow.HyperJSONGenerator;

public class Pages
{
    public List<string> GetPages(int number = -1)
    {
        if (number == -1)
        {
            number = pages.Count;
        }

        return pages.GetRange(0, number);
    }

    private List<string> pages = new()
    {
        "/home",
        "/about",
        "/contact",
        "/books",
        "/authors",
        "/bestsellers",
        "/highest-rate",
        "/shop",
        "/book",
        "/historical-books",
        "/fiction-books",
        "/non-fiction-books",
        "/dotnet-books",
        "/javascript-books",
        "/operating-system-books",
        "/memory-management-books",
        "/java-books",
        "/software-engineering-books",
        "/dotnet-5",
        "/dotnet-6",
        "/dotnet-7",
        "/dotnet-8",
        "/dotnet-9",
        "/dotnet-10",
    };
}
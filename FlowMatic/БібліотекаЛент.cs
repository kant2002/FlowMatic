namespace FlowMatic;

public class БібліотекаЛент
{
    private List<Лента> ленти = new();
    public IReadOnlyList<Лента> Ленти => ленти;

    public void ДодатиЛенту(string назва, string зміст)
    {
        var існуючаЛента = ленти.FirstOrDefault(_ => _.Назва == назва);
        if (існуючаЛента is not null)
        {
            throw new InvalidOperationException($"Reel with label {назва} already exists.");
        }

        ленти.Add(new (назва, зміст));
    }

    public void ВидалитиЛенту(string назва)
    {
        ленти.RemoveAll(_ => _.Назва == назва);
    }
}

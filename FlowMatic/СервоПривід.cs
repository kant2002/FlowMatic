namespace FlowMatic;

public class СервоПривід
{
    private int позиція;
    private string? дані;
    private Лента лента;
    public string Назва { get; private set; }
    public bool КінецьДаних;
    public void ВставитиЛенту(Лента дані)
    {
        this.лента = дані;
        this.дані = дані.Зміст;
        this.Назва = дані.Назва;
        this.позиція = 0;
    }

    public bool ЛентаВставлена => дані is not null;

    public string Прочитати(int кількість)
    {
        if (дані is null || КінецьДаних)
        {
            КінецьДаних = true;
            return "";
        }

        if (this.позиція + кількість >= this.дані.Length)
        {
            КінецьДаних = true;
        }

        if (this.позиція + кількість > this.дані.Length)
        {
            return "";
        }

        кількість = Math.Min(this.дані.Length - 1, this.позиція + кількість);
        var результат = дані.Substring(this.позиція, кількість - this.позиція);
        this.позиція += кількість;
        return результат;
    }

    public void Записати(string дані)
    {
        this.дані += дані;
        this.позиція += дані.Length;
    }

    public void Перемотати()
    {
        this.позиція = 0;
        КінецьДаних = false;
    }

    public Лента ВитягнутиЛенту()
    {
        var результат = this.лента with { Зміст = дані };
        позиція = 0;
        дані = null;
        КінецьДаних = true;
        return результат;
    }

    internal void Відмотати()
    {
        позиція = 0;
        КінецьДаних = false;
    }
}

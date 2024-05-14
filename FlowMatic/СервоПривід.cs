namespace FlowMatic;

public class СервоПривід
{
    private int позиція;
    private string дані;
    public bool КінецьДаних;
    public void ВставитиЛенту(string дані)
    {
        this.дані = дані;
        this.позиція = 0;
    }

    public string Прочитати(int кількість)
    {
        if (this.позиція + кількість >= this.дані.Length)
        {
            КінецьДаних = true;
        }

        кількість = Math.Min(this.дані.Length - 1, this.позиція + кількість);
        return дані.Substring(this.позиція, кількість);
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
}

﻿namespace FlowMatic;

public class СервоПривід
{
    private int позиція;
    private string дані;
    public string Назва { get; private set; }
    public bool КінецьДаних;
    public void ВставитиЛенту(Лента дані)
    {
        this.дані = дані.Зміст;
        this.Назва = дані.Назва;
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

using FlowMatic.Юнівак;
using static FlowMatic.Приклади;

namespace FlowMatic.Tests;

public class ТестВіртуальноїМашини
{

    [Fact]
    public void КопіюванняЗапису()
    {
        var к = new Компілятор();
        var код =
            """
            (0) INPUT PRICE FILE-A ; OUTPUT PRICE FILE-B .
            (1) TRANSFER A TO B .
            (2) WRITE-ITEM B .
            (3) CLOSE-OUT FILES B .
            (4) STOP. (END)
            """;
        var програма = к.Скомпілювати(код);

        var вм = new ВіртуальнаМашина();
        List<Лента> витягнутиЛенти = new();
        вм.ЛентаВитягнута += (лента) =>
        {
            витягнутиЛенти.Add(лента);
        };
        вм.СкомпілюватиПрограму(new ЛентаВводу() { ІсходнийКод = код, Файли = [Ціна] });
        вм.СервоПривіди[0].ВставитиЛенту(new Лента("PRICE", "PRICE", "           1            ZZZZZZZZZZZZ              "));
        var вихіднаЛента = new Лента("NEW_PRICE", "PRICE", "");
        вм.СервоПривіди[1].ВставитиЛенту(вихіднаЛента);

        вм.ВиконатиПрограму();

        var лента = Assert.Single(витягнутиЛенти);
        Assert.Equal("NEW_PRICE", лента.Назва);
        Assert.Equal("           1            ZZZZZZZZZZZZ", лента.Зміст);
    }

    [Fact]
    public void Стрибок()
    {
        var к = new Компілятор();
        var код =
            """
            (0) INPUT PRICE FILE-A ; OUTPUT PRICE FILE-B .
            (1) JUMP TO OPERATION 3 .
            (2) READ-ITEM A .
            (3) TRANSFER A TO B .
            (4) WRITE-ITEM B .
            (5) CLOSE-OUT FILES B .
            (6) STOP. (END)
            """;
        var програма = к.Скомпілювати(код);

        var вм = new ВіртуальнаМашина();
        List<Лента> витягнутиЛенти = new();
        вм.ЛентаВитягнута += (лента) =>
        {
            витягнутиЛенти.Add(лента);
        };
        вм.СкомпілюватиПрограму(new ЛентаВводу() { ІсходнийКод = код, Файли = [Ціна] });
        вм.СервоПривіди[0].ВставитиЛенту(new Лента("PRICE", "PRICE", "           1            ZZZZZZZZZZZZ              "));
        var вихіднаЛента = new Лента("NEW_PRICE", "PRICE", "");
        вм.СервоПривіди[1].ВставитиЛенту(вихіднаЛента);

        вм.ВиконатиПрограму();

        var лента = Assert.Single(витягнутиЛенти);
        Assert.Equal("NEW_PRICE", лента.Назва);
        Assert.Equal("           1            ZZZZZZZZZZZZ", лента.Зміст);
    }

    [Fact]
    public void ПерехідПриКінціДаних()
    {
        var к = new Компілятор();
        var код =
            """
            (0) INPUT PRICE FILE-A PRICE FILE-B ; OUTPUT PRICE FILE-C .
            (1) TRANSFER A TO C .
            (2) WRITE-ITEM C .
            (3) READ-ITEM A ; IF END OF DATA GO TO OPERATION 5 .
            (4) JUMP TO OPERATION 1 .
            (5) TRANSFER B TO C .
            (6) WRITE-ITEM C .
            (7) CLOSE-OUT FILES C .
            (8) STOP. (END)
            """;
        var програма = к.Скомпілювати(код);

        var вм = new ВіртуальнаМашина();
        List<Лента> витягнутиЛенти = new();
        вм.ЛентаВитягнута += (лента) =>
        {
            витягнутиЛенти.Add(лента);
        };
        вм.СкомпілюватиПрограму(new ЛентаВводу() { ІсходнийКод = код, Файли = [Ціна] });
        вм.СервоПривіди[0].ВставитиЛенту(new Лента("PRICE", "PRICE", "_          1            ZZZZZZZZZZZZ              "));
        вм.СервоПривіди[1].ВставитиЛенту(new Лента("PRICE", "PRICE", "_          2            ZZZZZZZZZZZZ              "));
        var вихіднаЛента = new Лента("NEW_PRICE", "PRICE", "");
        вм.СервоПривіди[2].ВставитиЛенту(вихіднаЛента);

        вм.ВиконатиПрограму();

        var лента = Assert.Single(витягнутиЛенти);
        Assert.Equal("NEW_PRICE", лента.Назва);
        Assert.Equal("_          1            _          2            ZZZZZZZZZZZZ", лента.Зміст);
    }
}

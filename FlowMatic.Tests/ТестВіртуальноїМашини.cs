using FlowMatic.Юнівак;
using static FlowMatic.Приклади;

namespace FlowMatic.Tests;

public class ТестВіртуальноїМашини
{

    [Fact]
    public void КопыюванняЗапису()
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
}

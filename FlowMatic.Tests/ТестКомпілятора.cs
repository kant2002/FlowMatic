using FlowMatic.Операції;

namespace FlowMatic.Tests;

public class ТестКомпілятора
{
    [Fact]
    public void InputПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) INPUT INVENTORY FILE-A PRICE FILE-B ; OUTPUT PRICED-INV FILE-C UNPRICED-INV FILE-D ; HSP D .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Input>(операція);
        Assert.Equal([new("INVENTORY", 'A', []), new("PRICE", 'B', [])], input.ВхідніФайли);
        Assert.Equal([new("PRICED-INV", 'C', []), new("UNPRICED-INV", 'D', [])], input.ВихідніФайли);
        Assert.Equal(['D'], input.ВисокошвидкосніПрінтери);
    }
}
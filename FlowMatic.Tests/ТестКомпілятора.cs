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
        Assert.Equal(new char[] { 'D' }, input.ВисокошвидкосніПрінтери);
    }

    [Fact]
    public void CompareПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (B) ; IF GREATER GO TO OPERATION 10 ; IF EQUAL GO TO OPERATION 5 ; OTHERWISE GO TO OPERATION 2 .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Compare>(операція);
        Assert.Equal(new("PRODUCT-NO", 'A'), input.Перший);
        Assert.Equal(new("PRODUCT-NO", 'B'), input.Другий);
        Assert.Equal((ushort)10, input.Більше);
        Assert.Equal((ushort)5, input.Рівно);
        Assert.Equal((ushort)2, input.Інакше);
    }

    [Fact]
    public void TransferПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) TRANSFER A TO D .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Transfer>(операція);
        Assert.Equal('A', input.ІсходнийФайл);
        Assert.Equal('D', input.ЦільовийФайл);
    }

    [Fact]
    public void WriteItemПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) WRITE-ITEM D .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<WriteItem>(операція);
        Assert.Equal('D', input.Файл);
    }

    [Fact]
    public void JumpПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) JUMP TO OPERATION 8 .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Jump>(операція);
        Assert.Equal((ushort)8, input.Операція);
    }

    [Fact]
    public void ReadItemПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) READ-ITEM A ; IF END OF DATA GO TO OPERATION 14 .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<ReadItem>(операція);
        Assert.Equal('A', input.Файл);
        Assert.Equal((ushort)14, input.Кінець);
    }

    [Fact]
    public void SetOperationПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) SET OPERATION 9 TO GO TO OPERATION 2 .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<SetOperation>(операція);
        Assert.Equal((ushort)9, input.Операція);
        Assert.Equal((ushort)2, input.НоваОперація);
    }

    [Fact]
    public void TestПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) TEST PRODUCT-NO (B) AGAINST ZZZZZZZZZZZZ ; IF EQUAL GO TO OPERATION 16 ; OTHERWISE GO TO OPERATION 15 .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Test>(операція);
        Assert.Equal(new("PRODUCT-NO", 'B'), input.Поле);
        var порівняння = input.Порівняння[0];
        Assert.Equal("ZZZZZZZZZZZZ", порівняння.Значення);
        Assert.Equal((ushort)16, порівняння.Рівно);
        Assert.Equal((ushort)15, порівняння.Інакше);
        Assert.Null(порівняння.Більше);
        Assert.Null(порівняння.Менше);
        Assert.Null(порівняння.НеРівне);
    }

    [Fact]
    public void RewindПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) REWIND B .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Rewind>(операція);
        Assert.Equal(new char[] { 'B' }, input.Файли);
    }

    [Fact]
    public void CloseOutПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) CLOSE-OUT FILES C , D .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<CloseOut>(операція);
        Assert.Equal(new char[] { 'C', 'D' }, input.Файли);
    }

    [Fact]
    public void StopПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) STOP. (END) ";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Stop>(операція);
    }

    [Fact]
    public void MoveПарсінг()
    {
        var к = new Компілятор();
        var код = "(0) MOVE UNIT-PRICE (B) TO UNIT-PRICE (C) .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Move>(операція);
        var описПереміщення = Assert.Single(input.ОписПереміщення);
        Assert.Equal(new ПосиланняНаПоле("UNIT-PRICE", 'B'), описПереміщення.ІсходнеПоле);
        Assert.Equal([new("UNIT-PRICE", 'C')], описПереміщення.ЦільовіПоля);
    }

    [Fact]
    public void MoveПарсінг2()
    {
        var к = new Компілятор();
        var код = "(0) MOVE PRODUCT-NO (A) TO PRODUCT-NO (W) ; QUANTITY (A) TO QUANTITY (W) .";

        var програма = к.Скомпілювати(код);

        var операція = програма.Операції[0];
        var input = Assert.IsType<Move>(операція);
        Assert.Equal(2, input.ОписПереміщення.Length);
        Assert.Equal([
            (new ПосиланняНаПоле("PRODUCT-NO", 'A'), [new ПосиланняНаПоле("PRODUCT-NO", 'W')]),
            (new ПосиланняНаПоле("QUANTITY", 'A'), [new ПосиланняНаПоле("QUANTITY", 'W')]),
        ], input.ОписПереміщення);
    }
}
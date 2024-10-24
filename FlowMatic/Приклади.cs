using FlowMatic.Юнівак;

namespace FlowMatic;

public static class Приклади
{

    public static ДізайнФайлу Інвентар = new ДізайнФайлу()
    {
        НазваФайлу = "INVENTORY",
        Мітка = "MMDDYY100101",
        МісцеМітки = 3,
        БагатоБобінний = true,
        ІндікаторКількостіБлоків = true,
        МісцеКількостіБлоків = 1,
        ДізайнЕлементів = new ДізайнЕлементів()
        {
            РозмірЕлемента = 10,
            Ключи = ["PRODUCT-NO"]
        },
        ДізайнПолей = new ДізайнПолей()
        {
            Поля = new[]
            {
                new ОписПоля()
                {
                    Назва = "PRODUCT-NO",
                    Тип = ТипПоля.ЧислаТаЛітери,
                    Місце = 0,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 1,
                    КількістьСимволів = 12,
                },
                new ОписПоля()
                {
                    Назва = "QUANTITY",
                    Тип = ТипПоля.Числовий,
                    Місце = 1,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 7,
                    КількістьСимволів = 6,
                    Витягач = "000000111111",
                },
            }
        },
    };

    public static ДізайнФайлу Ціна = new ДізайнФайлу()
    {
        НазваФайлу = "PRICE",
        Мітка = "MMDDYY100201",
        МісцеМітки = 3,
        БагатоБобінний = false,
        МаркерКінцяБобіни = "",
        ІндікаторКількостіБлоків = true,
        МісцеКількостіБлоків = 1,
        ДізайнЕлементів = new ДізайнЕлементів()
        {
            РозмірЕлемента = 2,
            Ключи = ["PRODUCT-NO"]
        },
        ДізайнПолей = new ДізайнПолей()
        {
            Поля = new[]
            {
                new ОписПоля()
                {
                    Назва = "PRODUCT-NO",
                    Тип = ТипПоля.ЧислаТаЛітери,
                    Місце = 0,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 1,
                    КількістьСимволів = 12,
                },
                new ОписПоля()
                {
                    Назва = "UNIT-PRICE",
                    Тип = ТипПоля.Числовий,
                    Місце = 1,
                    ПозіціяТочки = "3R",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 8,
                    КількістьСимволів = 5,
                    Витягач = "000000011111",
                },
            }
        },
    };

    public static ДізайнФайлу ОціненийІнвентар = new ДізайнФайлу()
    {
        НазваФайлу = "PRICED-INV",
        Мітка = "MMDDYY100301",
        МісцеМітки = 3,
        БагатоБобінний = true,
        ІндікаторКількостіБлоків = true,
        МісцеКількостіБлоків = 1,
        ДізайнЕлементів = new ДізайнЕлементів()
        {
            РозмірЕлемента = 10,
            Ключи = ["PRODUCT-NO"]
        },
        ДізайнПолей = new ДізайнПолей()
        {
            Поля =
            [
                new ОписПоля()
                {
                    Назва = "PRODUCT-NO",
                    Тип = ТипПоля.ЧислаТаЛітери,
                    Місце = 0,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 1,
                    КількістьСимволів = 12,
                },
                new ОписПоля()
                {
                    Назва = "QUANTITY",
                    Тип = ТипПоля.Числовий,
                    Місце = 1,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 7,
                    КількістьСимволів = 6,
                    Витягач = "000000111111",
                },
                new ОписПоля()
                {
                    Назва = "UNIT-PRICE",
                    Тип = ТипПоля.Числовий,
                    Місце = 2,
                    ПозіціяТочки = "3R",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 8,
                    КількістьСимволів = 5,
                    Витягач = "000000011111",
                },
                new ОписПоля()
                {
                    Назва = "EXT-PRICE",
                    Тип = ТипПоля.Числовий,
                    Місце = 3,
                    ПозіціяТочки = "8R",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 3,
                    КількістьСимволів = 10,
                    Витягач = "001111111111",
                },
            ]
        },
    };

    public static ДізайнФайлу НеоціненийІнвентар = new ДізайнФайлу()
    {
        НазваФайлу = "UNPRICED-INV",
        Мітка = "MMDDYY100401",
        МісцеМітки = 3,
        БагатоБобінний = true,
        ІндікаторКількостіБлоків = true,
        МісцеКількостіБлоків = 1,
        ДізайнЕлементів = new ДізайнЕлементів()
        {
            РозмірЕлемента = 10,
            Ключи = ["PRODUCT-NO"]
        },
        ДізайнПолей = new ДізайнПолей()
        {
            Поля =
            [
                new ОписПоля()
                {
                    Назва = "PRODUCT-NO",
                    Тип = ТипПоля.ЧислаТаЛітери,
                    Місце = 0,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 1,
                    КількістьСимволів = 12,
                },
                new ОписПоля()
                {
                    Назва = "QUANTITY",
                    Тип = ТипПоля.Числовий,
                    Місце = 1,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 7,
                    КількістьСимволів = 6,
                    Витягач = "000000111111",
                },
            ]
        },
    };

    public static ДізайнФайлу Помилка = new ДізайнФайлу()
    {
        НазваФайлу = "ERROR",
        Мітка = "MMDDYY100501",
        МісцеМітки = 3,
        БагатоБобінний = true,
        ІндікаторКількостіБлоків = true,
        МісцеКількостіБлоків = 1,
        ДізайнЕлементів = new ДізайнЕлементів()
        {
            РозмірЕлемента = 10,
            Ключи = []
        },
    };

    public static РобочаОбласть РобочаОбластьЗадачи3 = new ()
    {
        ДізайнПолей = new ДізайнПолей()
        {
            Поля =
            [
                new ОписПоля()
                {
                    Назва = "PRODUCT-NO",
                    Тип = ТипПоля.ЧислаТаЛітери,
                    Місце = 0,
                    ПозіціяТочки = "",
                    ПозіціяЗнака = null,
                    ПозіціяНайлівішогоСимвола = 1,
                    КількістьСимволів = 12,
                },
            ]
        },
    };

    public static ЛентаВводу Приклад1 = new()
    {
        Файли = [Інвентар, Ціна, ОціненийІнвентар, НеоціненийІнвентар],
        ІсходнийКод =
        """
        (0) INPUT INVENTORY FILE-A PRICE FILE-B ; OUTPUT PRICED-INV FILE-C UNPRICED-INV FILE-D ; HSP D .
        (1) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (B) ; IF GREATER GO TO OPERATION 10 ; IF EQUAL GO TO OPERATION 5 ; OTHERWISE GO TO OPERATION 2 .
        (2) TRANSFER A TO D .
        (3) WRITE-ITEM D .
        (4) JUMP TO OPERATION 8 .
        (5) TRANSFER A TO C .
        (6) MOVE UNIT-PRICE (B) TO UNIT-PRICE (C) .
        (7) WRITE-ITEM C .
        (8) READ-ITEM A ; IF END OF DATA GO TO OPERATION 14 .
        (9) JUMP TO OPERATION 1 .
        (10) READ-ITEM B ; IF END OF DATA GO TO OPERATION 14 .
        (11) JUMP TO OPERATION 1 .
        (12) SET OPERATION 9 TO GO TO OPERATION 2 .
        (13) JUMP TO OPERATION 2 .
        (14) TEST PRODUCT-NO (B) AGAINST ZZZZZZZZZZZZ ; IF EQUAL GO TO OPERATION 16 ; OTHERWISE GO TO OPERATION 15 .
        (15) REWIND B .
        (16) CLOSE-OUT FILES C, D .
        (17) STOP. (END)
        """
    };

    public static ЛентаВводу Приклад2 = new()
    {
        Файли = [Інвентар, Ціна, ОціненийІнвентар, НеоціненийІнвентар, Помилка],
        ІсходнийКод =
        """
        (0) INPUT INVENTORY FILE-A PRICE FILE-B ; OUTPUT PRICED-INV FILE-C UNPRICED-INV FILE-D ERROR FILE-E ; HSP D, E .
        (1) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (B) ; IF GREATER GO TO OPERATION 14 ; IF EQUAL GO TO OPERATION 5 ; OTHERWISE GO TO OPERATION 2 .
        (2) TRANSFER A TO D .
        (3) WRITE-ITEM D .
        (4) JUMP TO OPERATION 8 .
        (5) TRANSFER A TO C .
        (6) MOVE UNIT-PRICE (B) TO UNIT-PRICE (C) .
        (7) WRITE-ITEM C .
        (8) MOVE PRODUCT-NO (A) TO PRODUCT-NO (W) .
        (9) READ-ITEM A ; IF END OF DATA GO TO OPERATION 16 .
        (10) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (W) ; IF EQUAL GO TO OPERATION 11 ; OTHERWISE GO TO OPERATION 1 .
        (11) TRANSFER A TO E .
        (12) WRITE-ITEM E .
        (13) JUMP TO OPERATION 9 .
        (14) READ-ITEM B ; IF END OF DATA GO TO OPERATION 1 .
        (15) JUMP TO OPERATION 1 .
        (16) TEST PRODUCT-NO (B) AGAINST ZZZZZZZZZZZZ ; IF EQUAL GO TO OPERATION 16 ; OTHERWISE GO TO OPERATION 18 .
        (17) REWIND B .
        (18) CLOSE-OUT FILES C, D, E .
        (19) STOP. (END)
        """
    };

    public static ЛентаВводу Приклад3 = new()
    {
        Файли = [Інвентар, Ціна, ОціненийІнвентар, НеоціненийІнвентар],
        РобочаОбласть = РобочаОбластьЗадачи3,
        Діректорія = new()
        {
            МаксімальнийАдрес = 0,
        },
        ІсходнийКод =
        """
        (0) INPUT INVENTORY FllE-A PRICE FllE-B ; OUTPUT PRICED-INV FILE-C UNPRICED-INV FllE-D ; HSP D . 
        (I) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (B) ; IF GREATER GO TO OPERATION  21 ; IF EQUAL GO TO OPERATION 5 ; OTHERWISE GO TO OPERATION 2 . 
        (2) TRANSFER A TO D . 
        (3) SET OPERATION 13 TO GO TO OPERATION 18 . 
        (4) JUMP TO OPERATION 8 . 
        (5) TRANSFER A TO C . 
        (6) MOVE UNIT-PRICE (B) TO UNIT-PRICE (C) . 
        (7) SET OPERATION 13 TO GO TO OPERATION 14 . 
        (8) MOVE PRODUCT-NO (A) TO PRODUCT-NO (W) ; QUANTITY (A) TO QUANTITY (W) .
        (9) READ-ITEM A ; IF END OF DATA GO TO OPERATION 23 .
        (10) COMPARE PRODUCT-NO (A) WITH PRODUCT-NO (W) ; IF EQUAL GO TO OPERATION 11 ; OTHERWISE GO TO OPERATION 13 .
        (11) X-I ADD QUANTITY (A) TO STORED QUANTITY (W) . 
        (12) JUMP TO OPERATION 9 . 
        (13) JUMP TO OPERATION 14 . 
        (I4) MOVE QUANTITY (W) TO QUANTITY (C) , 
        (15) X-I COMPUTE EXTENDED PRICE AND INSERT IN C ITEM.
        (16) WRITE-ITEM C . 
        (I7) JUMP TO OPERATION 1 .
        (18) MOVE QUANTITY (W) TO QUANTITY (D) .
        (19) WRITE-ITEM D .
        (20) JUMP TO OPERATION 17 .
        (21) READ-ITEM B ; IF END OF DATA GO TO OPERATION 1 .
        (22) JUMP TO OPERATION 1 .
        (23) EXECUTE OPERATION 13 THROUGH OPERATION 17 . 
        (24) TEST PRODUCT-NO (8) AGAINST ZZZZZZZZZZZZ ; IF EQUAL GO TO OPERATION 26 ; OTHERWISE GO TO OPERATION 25 .
        (25) REWIND B .
        (26) CLOSE-OUT FILES C , D .
        (27) STOP. (END) 
        """
    };
}

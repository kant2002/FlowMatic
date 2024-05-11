﻿namespace FlowMatic;

public static class ПрикладиФайлів
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
            }
        },
    };

    public static ДізайнФайлу НеоціненийІнвентар = new ДізайнФайлу()
    {
        НазваФайлу = "INVENTORY",
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
}

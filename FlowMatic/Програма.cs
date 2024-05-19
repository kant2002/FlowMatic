using FlowMatic;
using Мікрософт.АспНетЯдро.Компоненти.Веб;
using Мікрософт.АспНетЯдро.Компоненти.ВебАсемблі.Хостінг;
using Microsoft.AspNetCore.Components.Web;

var будівник = БудівникХостаВебАсемблі.СтворитиЗаЗамовчанням(args);
будівник.КорневіКомпоненти.Add<Апка>("#app");
будівник.КорневіКомпоненти.Add<ВіддушинаЗаголовка>("head::after");
будівник.КорневіКомпоненти.Add<HeadOutlet>("head::after");

будівник.Сервіси.AddSingleton<БібліотекаЛент>();
будівник.Сервіси.AddScoped(sp => new HttpClient { BaseAddress = new Uri(будівник.ОточенняХоста.BaseAddress) });

await будівник.Побудувати().ЗапуститиАсінх();
namespace FlowMatic.Операції;

public record Test(ПосиланняНаПоле Поле, (string Значення, ushort? Більше, ushort? Рівно, ushort? Менше, ushort? НеРівне, ushort Інакше)[] Порівняння): ІОперація;

using Microsoft.JSInterop;

namespace FlowMatic;

public class БібліотекаЛент(IJSRuntime jsСередаВиконання)
{
    private List<Лента> ленти = new();
    public IReadOnlyList<Лента> Ленти => ленти;
    private bool завантажено;

    public async ValueTask ЗавантажитиІзЛокальногоСховища()
    {
        if (завантажено)
        {
            return;
        }

        var бобіни = await jsСередаВиконання.InvokeAsync<List<Лента>>("getReels", Array.Empty<object>());
        ленти.AddRange(бобіни);
        завантажено = true;
    }

    public async Task ДодатиЛенту(string назва, string зміст)
    {
        var існуючаЛента = ленти.FirstOrDefault(_ => _.Назва == назва);
        if (існуючаЛента is not null)
        {
            throw new InvalidOperationException($"Reel with label {назва} already exists.");
        }

        ленти.Add(new (назва, зміст));
        await ЗберігтиУЛокальномуСховищі();
    }

    public async Task ВидалитиЛенту(string назва)
    {
        ленти.RemoveAll(_ => _.Назва == назва);
        await ЗберігтиУЛокальномуСховищі();
    }

    private async Task ЗберігтиУЛокальномуСховищі()
    {
        await jsСередаВиконання.InvokeVoidAsync("setReels", [ленти]);
    }
}

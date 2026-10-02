using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KisiselHarcamaTakip.Web.ModelBinders;

public sealed class TutarModelBinder : IModelBinder
{
    private static readonly CultureInfo TurkishCulture = CultureInfo.GetCultureInfo("tr-TR");

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);
        var value = valueResult.FirstValue?.Trim();
        if (string.IsNullOrEmpty(value))
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Tutar zorunludur.");
            bindingContext.Result = ModelBindingResult.Success(0m);
            return Task.CompletedTask;
        }

        if (!decimal.TryParse(value, NumberStyles.Number, ResolveCulture(value), out var amount))
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Geçerli bir tutar girin.");
            bindingContext.Result = ModelBindingResult.Success(0m);
            return Task.CompletedTask;
        }

        if (amount <= 0)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Tutar sıfırdan büyük olmalıdır.");
        }

        bindingContext.Result = ModelBindingResult.Success(amount);
        return Task.CompletedTask;
    }

    private static CultureInfo ResolveCulture(string value)
    {
        var commaIndex = value.LastIndexOf(',');
        var periodIndex = value.LastIndexOf('.');

        if (commaIndex >= 0 && periodIndex >= 0)
        {
            return commaIndex > periodIndex ? TurkishCulture : CultureInfo.InvariantCulture;
        }

        return commaIndex >= 0 ? TurkishCulture : CultureInfo.InvariantCulture;
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace KartoshkaEvent.Application.Common.ModelsBinders
{
    public class JsonFormDataModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            try
            {
                var result = JsonSerializer.Deserialize(value, bindingContext.ModelType, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (JsonException ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
            }

            return Task.CompletedTask;
        } 
    }
}

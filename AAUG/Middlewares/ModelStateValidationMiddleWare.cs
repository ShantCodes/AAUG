// using Microsoft.AspNetCore.Mvc.ModelBinding;
// using Newtonsoft.Json;

// namespace AAUG.Api;

// public class ModelStateValidationMiddleWare
// {
//     private readonly RequestDelegate _next;

//     public ModelStateValidationMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(HttpContext context)
//     {
//         if (context.Request.Method == HttpMethods.Post ||
//             context.Request.Method == HttpMethods.Put ||
//             context.Request.Method == HttpMethods.Patch)
//         {
//             context.Request.EnableBuffering();
//             var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
//             context.Request.Body.Position = 0;

//             if (!string.IsNullOrEmpty(body))
//             {
//                 var modelState = new ModelStateDictionary();

//                 var modelBindingProvider = context.RequestServices.GetRequiredService<IModelBinderFactory>();
//                 var modelMetadataProvider = context.RequestServices.GetRequiredService<IModelMetadataProvider>();

//                 var binder = modelBindingProvider.CreateBinder(new ModelBinderFactoryContext
//                 {
//                     Metadata = modelMetadataProvider.GetMetadataForType(typeof(object)),
//                     CacheToken = modelMetadataProvider.GetMetadataForType(typeof(object))
//                 });

//                 var bindingContext = DefaultModelBindingContext.CreateBindingContext(
//                     context,
//                     new CompositeValueProvider(),
//                     modelMetadataProvider.GetMetadataForType(typeof(object)),
//                     null,
//                     null
//                 );

//                 bindingContext.ModelState = modelState;
//                 bindingContext.ValueProvider = new CompositeValueProvider();

//                 await binder.BindModelAsync(bindingContext);

//                 if (!bindingContext.ModelState.IsValid)
//                 {
//                     context.Response.StatusCode = StatusCodes.Status400BadRequest;
//                     context.Response.ContentType = "application/json";
//                     var errors = bindingContext.ModelState
//                         .Where(x => x.Value.Errors.Count > 0)
//                         .Select(x => new { x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
//                         .ToList();
//                     await context.Response.WriteAsync(JsonConvert.SerializeObject(errors));
//                     return;
//                 }
//             }
//         }

//         await _next(context);
//     }

// }

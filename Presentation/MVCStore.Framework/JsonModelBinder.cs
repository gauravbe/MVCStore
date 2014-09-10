using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCStore.Framework
{
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        public class JsonModelBinder : IModelBinder
        {
            public object BindModel(
                ControllerContext controllerContext,
                ModelBindingContext bindingContext)
            {
                try
                {
                    var json = controllerContext.HttpContext.Request
                               .Params[bindingContext.ModelName];

                    if (string.IsNullOrWhiteSpace(json))
                        return null;

                    // Swap this out with whichever Json deserializer you prefer.
                    return Newtonsoft.Json.JsonConvert
                           .DeserializeObject(json, bindingContext.ModelType);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}

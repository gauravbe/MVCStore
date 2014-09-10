using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCStore.Admin.Models
{
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        public class JsonModelBinder : IModelBinder
        {
            private  readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();

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
                    return serializer.Deserialize(json, bindingContext.ModelType);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}

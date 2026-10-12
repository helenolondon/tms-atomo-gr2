using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace AtmApi.Controllers.Autenticacao.ActionFilters
{
    public class AutorizarUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues values;
            StringValues cookies;
            string token = null;

            context.HttpContext.Request.Headers.TryGetValue("token", out values);
            context.HttpContext.Request.Headers.TryGetValue("cookie", out cookies);

            if(values.Count > 0)
            {
                token = values[0];
            };

            if(token == null && cookies.Count > 0)
            {
                foreach(var cookieItem in cookies)
                {
                    var cookie = cookieItem.Split("=");

                    if(cookie[0] == "token")
                    {
                        token = cookie[1];
                        break;
                    };
                };
            };

            if (token != null)
            {
                if ((context.Controller as AtmController).services.SegurancaService.VerificarToken(token))
                {
                    // Set user information here
                    return;
                };
            };

            context.Result = new UnauthorizedObjectResult("Acesso não authorizado");
        }
    }
}

using KingPriceDemo.Application.Common.Security;

namespace KingPriceDemo.WebApi.Common.Middlewares
{
    public class UserIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, IInfoSetter infoSetter)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var claims = context.User.Claims;

                infoSetter.SetUser(claims);
            }

            return _next(context);
        }
    }
}

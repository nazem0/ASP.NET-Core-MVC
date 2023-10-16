using Microsoft.AspNetCore.Http;

namespace Web_App.Middlewares
{
    public class CategoryMiddleware
    {
        public RequestDelegate next;
        public CategoryMiddleware(RequestDelegate _next)
        {
            this.next = _next;
        }
        //public Task InvokeAsync(HttpContext Context)
        //{
        //    MyDBContext MyDB = new MyDBContext();
        //    if (Context.Request.Path == "/category")
        //        return Context.Response.WriteAsJsonAsync(MyDB.Categories);

        //    return next(Context);
        //}
    }
}

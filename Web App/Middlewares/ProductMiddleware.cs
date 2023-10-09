using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

public class ProductMiddleware
{
    public RequestDelegate next;
    public ProductMiddleware(RequestDelegate _next)
    {
        next = _next;
    }
    public Task InvokeAsync(HttpContext Context)
    {
        MyDBContext MyDB = new MyDBContext();
        if (Context.Request.Path == "/product")
        {
            if (Context.Request.Query["ID"].ToString() != "")
            {
                return Context.Response.WriteAsJsonAsync(MyDB.Products.Where(i => i.ID == int.Parse(Context.Request.Query["ID"])));
            }
            else
            {
                return Context.Response.WriteAsJsonAsync(MyDB.Products);
            }
        }
        return next(Context);
    }
}


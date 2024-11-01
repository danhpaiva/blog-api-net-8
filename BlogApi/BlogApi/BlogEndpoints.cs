using Microsoft.EntityFrameworkCore;
using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace BlogApi;

public static class BlogEndpoints
{
    public static void MapBlogEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Blog").WithTags(nameof(Blog));

        group.MapGet("/", async (BlogApiContext db) =>
        {
            return await db.Blog.ToListAsync();
        })
        .WithName("GetAllBlogs")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Blog>, NotFound>> (int id, BlogApiContext db) =>
        {
            return await db.Blog.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Blog model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Blog blog, BlogApiContext db) =>
        {
            var affected = await db.Blog
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, blog.Id)
                    .SetProperty(m => m.Nome, blog.Nome)
                    .SetProperty(m => m.Url, blog.Url)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        group.MapPost("/", async (Blog blog, BlogApiContext db) =>
        {
            db.Blog.Add(blog);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Blog/{blog.Id}",blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, BlogApiContext db) =>
        {
            var affected = await db.Blog
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}

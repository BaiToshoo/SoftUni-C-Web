using ForumApp.infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.infrastructure.Data.Configuration;
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    private Post[] initialPosts = new Post[]
    {
        new Post
        {
            Id = 1,
            Title = "First Post",
            Content = "This is the first post"
        },
        new Post
        {
            Id = 2,
            Title = "Second Post",
            Content = "This is the second post"
        },
        new Post
        {
            Id = 3,
            Title = "Third Post",
            Content = "This is the third post"
        }
    };
    public void Configure(EntityTypeBuilder<Post> builder)
    {
       builder.HasData(initialPosts);
    }
}

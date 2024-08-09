using Microsoft.EntityFrameworkCore;

namespace TestEF;

class Program
{
    static void Main(string[] args)
    {
    using (var dbc = new BloggingContext()) {

        var post1 = new Post()
        {
            Title = $"Свежие новости в {DateTime.Now.ToLongTimeString()}",
            Content = $"Самые свежие новости в {DateTime.Now.ToLongTimeString()}",
        };
        dbc.Posts.Add(post1);

        var post2 = new Post()
        {
            Title = $"Слухи о... в {DateTime.Now.ToLongTimeString()}",
            Content = $"Самые свежие cлухи о... в {DateTime.Now.ToLongTimeString()}",
        };
        dbc.Posts.Add(post2);

        var blog = new Blog()
        {
            Name = $"Блог номер {DateTime.Now.Millisecond} мирного обитателя дивана",
            Posts = new List<Post> { post1, post2 },
        };

        dbc.Blogs.Add(blog);

        dbc.SaveChanges();
    }

    Console.WriteLine("Data saved!");

    }
}


public class BloggingContext : DbContext 
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    {
        options.UseSqlServer("Password=******;Persist Security Info=True;User ID=username;Initial Catalog=Blogs;Data Source=192.168.1.1;TrustServerCertificate=True");
    }
}

public class Blog {
    public int BlogId { get; set; }
    public string Name { get; set; }

    public virtual List<Post> Posts { get; set; }
}

public class Post {
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public virtual Blog Blog { get; set; }
}

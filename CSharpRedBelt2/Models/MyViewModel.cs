#pragma warning disable CS8618

namespace CSharpRedBelt2.Models;
public class MyViewModel
{
    public User User {get; set;}
    public List<User> AllUsers {get; set;}

    public Post Post {get; set;}
    public List<Post> AllPosts {get; set;}
    
    public LikedPost LikedPost {get; set;}
    public List<LikedPost> AllLikedPosts {get; set;} 
}
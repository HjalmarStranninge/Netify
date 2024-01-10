using Microsoft.AspNetCore.SignalR;

namespace NetifyAPI.Handlers
{
    public class UserHandler
    {
        //// Get all users
        //public static IResult ListUsers(NetifyContext context)
        //{
        //    UserViewModel[] result = context.Users
        //        //.Include?? vad vill vi inkludera i listvy?
        //        .Select(u => new UserViewModel()
        //        {

        //        })
        //        .ToArray();
        //    return Results.Json(result);
        //}

        //// Search user
        //public static IResult SearchUsers(NetifyContext context, string query)
        //{
        //    var result = context.Users
        //        .Where(u = u => u.UserId.Contains(query)
        //        .Select(u => new UserViewModel()
        //        {
        //            // add what to show
        //        })
        //        .ToList();
        //    return Results.Json(result);
        //}

        
        //// View user
        //public static IResult ViewUser(NetifyContext context, string username)
        //{
        //    // checks if user exists
        //    context.Users
        //        .Where(u => u.UserId == userId)
        //        //.Include() vill vi visa upp x antal låtar/artister/genrer en user gillar? alt topp?
        //        .SingleOrDefault();
        //    if (u == null)
        //    {
        //        return Results.NotFound();
        //    }

        //    UserViewModel result = new UserViewModel()
        //    {
        //        UserId = u.UserId,
        //        // lägg till det vi vill visa
        //    }

        //}
        // Get all genres connected to a user

        // Get all artists connected to a user

        // Get all tracks connected to a user
    }
}

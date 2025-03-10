﻿class Program
{
  static void Main()
  {
    int port = 5000;

    User[] users = [];



    var server = new Server(port);

    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/index.html");



    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine($"Recieved a request with the path: {request.Path}");

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
          if (request.Path == "signup")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            string id = Guid.NewGuid().ToString();

            users = [..users, new User(username, password, id)]; //* עדכון למערך של משתמש חדש המכיל דברים שייחודיים לו *//

            Console.WriteLine(username + "," + password);
            response.Send(id);
          }  //*רישום משתמש חדש, שומר את פרטי המשתמש (שם משתמש, סיסמה) ומחזיר מזהה ייחודי עבור המשתמש החדש.*//

          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();

            bool founduser = false;
            string userId = ""; //*יועד לשמור את המזהה של המשתמש במידה והמשתמש נמצא במערכת.//*

            for (int i = 0; i < users.Length; i++)
            {
              if (username == users[i].username && password == users[i].password) //*השוואה בין שם המשתמש והסיסמה שהוזנו לאלה של כל משתמש במערך.*//
              {
                founduser = true;
                userId = users[i].id;
              }
            }

            response.Send((founduser, userId));
          } //*כניסת משתמש למערכת על ידי השוואת שם המשתמש והסיסמה שהוזנו לאלה של המשתמשים במערכת, ואם יש התאמה, מחזיר את המזהה של המשתמש.*//


         // else if (request.Path == "getUsername")
         // {
          //  string userId = request.GetBody<string>();

          //  int i = 0;
          //  while (users[i].id != userId)
          //  {
           //   i++;
          //  }
          //  string username = users[i].username;

           // response.Send(username);
          //} 

          else if (request.Path =="addToFavorites") 
          {
            (int i, string userId) = request.GetBody<(int, string)>();

            User user = default!;
            for (int j = 0; j < users.Length; j++)
            {
              if (userId == users[j].id)
              {
                user = users[j];
              }
            }

            user.favorites[i] = true;
          }


          else if (request.Path == "removeFromFavorites") 
          {
             (int i, string userId) = request.GetBody<(int, string)>();

            User user = default!;
            for (int j = 0; j < users.Length; j++)
            {
              if (userId == users[j].id)
              {
                user = users[j];
              }
            }

            
            user.favorites[i] = false;
          }

          else if (request.Path == "getfavorite") 
          {
            string userId = request.GetBody<string>();
            
             User user = default!;
            for (int j = 0; j < users.Length; j++)
            {
              if (userId == users[j].id)
              {
                user = users[j];
              }
            }

            response.Send(user.favorites);
          } 


          else
          {
            response.SetStatusCode(405);
          }
        }
        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }


      response.Close();
    }
  }
}

class User
{
  public string username;
  public string password;
  public string id;
  public bool[] favorites;

  public User(string username, string password, string id)
  {
    this.username = username;
    this.password = password;
    this.id = id;
    this.favorites = [false, false, false, false, false];
  }
}

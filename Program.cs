class Program
{
  static void Main()
  {
    int port = 5000;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids = [];

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
            usernames = [.. usernames, username];
            passwords = [.. passwords, password];
            string id = Guid.NewGuid().ToString();
            ids = [.. ids, id];
            Console.WriteLine(username + "," + password);
            response.Send(id);
          }

          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();

            bool founduser = false;
            string userId = "";

            for (int i = 0; i < usernames.Length; i++)
            {
              if (username == usernames[i] && password == passwords[i])
              {
                founduser = true;
                userId = ids[i];
              }
            }

            response.Send((founduser, userId));
          }

          else if (request.Path == "getUsername")
          {
            string userId = request.GetBody<string>();

            int i = 0;
            while (ids[i] != userId)
            {
              i++;
            }
            string username = usernames[i];

            response.Send(username);
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
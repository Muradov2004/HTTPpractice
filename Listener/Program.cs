using Listener;
using System.Net;
using System.Text;
using System.Text.Json;

List<User> users = new()
{
    new(){ Name = "Salam",Surname = "Salammm"},
    new(){ Name = "Ali",Surname = "Muradov"},
    new(){ Name = "gsdfg",Surname = "sdfgsdfg"},
};


HttpListener listener = new();

listener.Prefixes.Add(@"http://localhost:27001/");

listener.Start();

while (true)
{
    var context = listener.GetContext();
    var request = context.Request;
    var response = context.Response;
    StreamWriter streamWriter = new(response.OutputStream);
    StreamReader reader = new(request.InputStream);

    if (request.HttpMethod == HttpMethod.Get.ToString())
    {

        StringBuilder usersString = new();
        var text = JsonSerializer.Serialize(users);
        streamWriter.WriteLine(text);

        streamWriter.Close();
    }
    else if (request.HttpMethod == HttpMethod.Post.ToString())
    {
        var id = User.gId;
        var newUser = JsonSerializer.Deserialize<User>(reader.ReadToEnd());
        newUser!.Id = id;
        users.Add(newUser);
        var text = JsonSerializer.Serialize(users);
        streamWriter.WriteLine(text);
        streamWriter.Close();
    }
    else if (request.HttpMethod == HttpMethod.Put.ToString())
    {
        var updateUser = JsonSerializer.Deserialize<User>(reader.ReadToEnd());
        User.gId--;
        users.FirstOrDefault(u => u.Id == updateUser!.Id)!.Name = updateUser!.Name;
        users.FirstOrDefault(u => u.Id == updateUser!.Id)!.Surname = updateUser!.Surname;
        var text = JsonSerializer.Serialize(users);
        streamWriter.WriteLine(text);
        streamWriter.Close();
    }
    else if (request.HttpMethod == HttpMethod.Delete.ToString())
    {
        var deleteId = Convert.ToInt32(reader.ReadToEnd());
        users.Remove(users.FirstOrDefault(u => u.Id == deleteId)!);
        var text = JsonSerializer.Serialize(users);
        streamWriter.WriteLine(text);
        streamWriter.Close();
    }
}

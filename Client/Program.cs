using Client;
using System.Text.Json;

HttpClient client = new();

int choise = Convert.ToInt32(Console.ReadLine());

var msg = new HttpRequestMessage();
msg.RequestUri = new Uri(@"http://localhost:27001/");
if (choise == 1) msg.Method = HttpMethod.Get;
else if (choise == 2)
{

    msg.Method = HttpMethod.Post;
    var userName = Console.ReadLine();
    var Surname = Console.ReadLine();
    User user = new() { Name = userName, Surname = Surname };
    msg.Content = new StringContent(JsonSerializer.Serialize(user));
}
else if (choise == 3)
{

    msg.Method = HttpMethod.Put;
    var Id = Convert.ToInt32(Console.ReadLine());
    var userName = Console.ReadLine();
    var Surname = Console.ReadLine();
    User user = new() { Id = Id, Name = userName, Surname = Surname };
    msg.Content = new StringContent(JsonSerializer.Serialize(user));
}
else if (choise == 4)
{
    msg.Method = HttpMethod.Delete;
    var Id = Console.ReadLine();
    msg.Content = new StringContent(Id!);
}




var response = await client.SendAsync(msg);

var text = await response.Content.ReadAsStringAsync();

Console.WriteLine(text);
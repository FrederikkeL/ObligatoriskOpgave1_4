using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("TCP Server:");


TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7);
listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Task.Run(() => HandleClient(socket));

}
listener.Stop();

int randomnumber(int min, int max)
{
    Random random = new Random();
    return random.Next(min, max);
}

void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    writer.WriteLine("You can now choose between three actions: Get a random number between two numbers, add two numbers together or subtract a number from another. To Choose Random reply random to this message, to choose add reply add, to choose subtract reply subtract, if you are done, reply quit");
    writer.Flush();

    string message = reader.ReadLine().ToLower();
    
    while (message.Trim() != "quit")
    {
        Console.WriteLine(message);
        if (message == "random")
        {
            writer.WriteLine("Choose a minumum value and a maximum number seperated by a space");
            writer.Flush();
            string[] numbers = reader.ReadLine().Split();
            int min = Int32.Parse(numbers[0]);
            int max = Int32.Parse(numbers[1]);
            writer.WriteLine("result is: "+randomnumber(min, max));
            writer.Flush();
        }
        else if (message == "add")
        {
            writer.WriteLine("Choose two numbers to add seperated by a space");
            writer.Flush();
            string[] numbers = reader.ReadLine().Split();
            int a = Int32.Parse(numbers[0]);
            int b = Int32.Parse(numbers[1]);
            writer.WriteLine("result is: "+(a + b));
            writer.Flush();
        }
        else if (message == "subtract")
        {
            writer.WriteLine("Choose a number and a number that will be subtracted seperated by a space");
            writer.Flush();
            string[] numbers = reader.ReadLine().Split();
            int a = Int32.Parse(numbers[0]);
            int b = Int32.Parse(numbers[1]);
            writer.WriteLine("result is: "+(a - b));
            writer.Flush();
        }
    

        

        writer.WriteLine("You can now choose a new action, if you are done, reply quit");
        writer.Flush();
        message = reader.ReadLine().ToLower();
    }
    socket.Close();



}


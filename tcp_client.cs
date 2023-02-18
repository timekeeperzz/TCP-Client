using System.Net.NetworkInformation;
using System.Net.Sockets;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter the IP that you want to connect: ");
            string IP = Console.ReadLine();
            Console.Write("Please enter the Port info that you want to connect: ");
            string Port = Console.ReadLine();
            int port = Convert.ToInt32(Port);

            string message = "1";
            Recieve(IP, port, message);
            Connect(IP, port, message);

            Console.WriteLine(" \n Datas are saved at C:\\");
            Console.WriteLine("\n Press Enter to continue...");




            Console.Read();
        }
        static void Recieve(string server, int port, string message)
        {

            string dosyaAdi = "clientRecieve.txt";
            string dosyaYolu = @"C:\Users\Hp\OneDrive\Masaüstü\kodlama\C# console\client";
            string hedefYol = System.IO.Path.Combine(dosyaYolu, dosyaAdi); // Printing client-server messages to txt file
            if (!System.IO.File.Exists(hedefYol))
            {
                System.IO.File.Create(hedefYol);
                Console.ReadKey();
            }
            while (message != "0") // Loops until user enters zero.
            {

                try
                {


                    TcpClient client1 = new TcpClient(server, port);

                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);


                    NetworkStream stream = client1.GetStream();
                    stream.Write(data, 0, data.Length);
                    // Send the message to the connected TcpServer.

                    data = new Byte[256];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    if (responseData == "0")
                        break;
                    Console.WriteLine(Program.CurrentTime + "Received: {0}", responseData);

                    // Close everything.
                    stream.Close();
                    client1.Close();
                    using (System.IO.StreamWriter dosya1 = new System.IO.StreamWriter(hedefYol, true)) // creating file
                    {
                        dosya1.Write(Program.CurrentTime + ":  Received from Server: {0}", responseData + "\n");
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(Program.CurrentTime + "ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(Program.CurrentTime + "SocketException: {0}", e);
                }

            }//end while

        }
        static void Connect(string server, int port, string message)
        {

            string dosyaAdi = "clientSent.txt";
            string dosyaYolu = @"C:\";
            string hedefYol = System.IO.Path.Combine(dosyaYolu, dosyaAdi); // Printing client-server messages to txt file
            if (!System.IO.File.Exists(hedefYol))
            {
                System.IO.File.Create(hedefYol);
                Console.ReadKey();
            }

            while (message != "0") // Loops until user enters zero.
            {

                Console.Write("Enter your message: "); //the message to be sent to the server

                message = Console.ReadLine();
                if (message == "0")
                    break;
                try
                {
                    TcpClient client2 = new TcpClient(server, port);

                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);


                    NetworkStream stream = client2.GetStream();

                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);

                    Console.WriteLine(Program.CurrentTime + "Sent: {0}", message);

                    // Receive the TcpServer.response.
                    // Buffer to store the response bytes.
                    // Close everything.
                    stream.Close();
                    client2.Close();
                    using (System.IO.StreamWriter dosya2 = new System.IO.StreamWriter(hedefYol, true)) // creating file
                    {
                        dosya2.Write(Program.CurrentTime + ":  Client sent: {0}", message + "\n");
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(Program.CurrentTime + "ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(Program.CurrentTime + "SocketException: {0}", e);
                }

            }//end while
        }//end connect

        public static DateTime CurrentTime = DateTime.Now;  // current time function
        public Program(int year, int month, int day)
        {
            DateTime Date = new DateTime(year, month, day);
        }
    }//end classs
}//end namespace


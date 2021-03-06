﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace RSASender
{
    class TcpClient
    {
        public void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = Encoding.UTF8.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[512];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.UTF8.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show($"{e}");
                //Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                MessageBox.Show($"{e}");
                
            }

           
        }
    }
}
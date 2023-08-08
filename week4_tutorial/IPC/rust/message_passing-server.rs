use std::io::{Read, Write};
use std::net::{TcpListener, TcpStream};

fn main() {
    // Replace with the actual IP address of the server
    let server_ip = "server_ip_here";
    let server_port = 12345;

    // Bind a TCP listener to the specified IP and port
    let listener = TcpListener::bind(format!("{}:{}", server_ip, server_port)).unwrap();
    println!("Server listening on {}:{}", server_ip, server_port);

    // Start accepting incoming connections in a loop
    for stream in listener.incoming() {
        match stream {
            Ok(mut stream) => {
                // Create a buffer to read incoming data
                let mut buffer = [0; 1024];
                match stream.read(&mut buffer) {
                    Ok(n) => {
                        // Convert received bytes to a string and print
                        let received_data = String::from_utf8_lossy(&buffer[..n]);
                        println!("Received: {}", received_data);
                    }
                    Err(e) => {
                        // Error occurred while reading data from the stream
                        println!("Error reading: {:?}", e);
                    }
                }
            }
            Err(e) => {
                // Error occurred while accepting a connection
                println!("Error accepting connection: {:?}", e);
            }
        }
    }
}

use std::io::Write;
use std::net::TcpStream;

fn main() {
    // Replace with the actual IP address of the server
    let server_ip = "server_ip_here";
    let server_port = 12345;

    // Attempt to establish a TCP connection to the server
    match TcpStream::connect(format!("{}:{}", server_ip, server_port)) {
        Ok(mut stream) => {
            // Message to send to the server
            let message = "Hello from Rust Client!";
            
            // Write the message as bytes to the stream
            match stream.write(message.as_bytes()) {
                Ok(_) => {
                    // Message sent successfully
                    println!("Message sent: {}", message);
                }
                Err(e) => {
                    // Error occurred while sending the message
                    println!("Error sending message: {:?}", e);
                }
            }
        }
        Err(e) => {
            // Error occurred while establishing the connection
            println!("Error connecting: {:?}", e);
        }
    }
}

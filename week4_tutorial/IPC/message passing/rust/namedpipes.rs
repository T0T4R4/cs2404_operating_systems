use std::io::{Read, Write};
use std::os::unix::net::UnixStream;
use std::thread;

fn main() {
    let pipe_path = "/tmp/my_named_pipe";

    // Create a named pipe client
    let mut client = UnixStream::connect(pipe_path).expect("Failed to connect to the named pipe");

    // Send a message to the server
    let message = "Hello from Rust Client!";
    client.write_all(message.as_bytes()).expect("Failed to write to the pipe");

    println!("Message sent: {}", message);

    // Read the response from the server
    let mut response = String::new();
    client.read_to_string(&mut response).expect("Failed to read from the pipe");

    println!("Received response: {}", response);
}

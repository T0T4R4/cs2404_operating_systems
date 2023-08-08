use std::io::{Read, Write};
use std::net::TcpStream;
use serde::{Deserialize, Serialize};
use bincode::{deserialize, serialize};

fn main() -> Result<(), Box<dyn std::error::Error>> {
    // Prepare arguments for the RPC call
    let args = (5, 7);
    let request = ("add".to_string(), args);

    // Serialize the request
    let serialized_request = serialize(&request)?;

    // Connect to the server
    let mut stream = TcpStream::connect("127.0.0.1:12345")?;

    // Send the serialized request to the server
    stream.write_all(&serialized_request)?;

    // Read the response from the server
    let mut buffer = [0u8; 1024];
    let bytes_read = stream.read(&mut buffer)?;

    // Deserialize and print the response
    let response: i32 = deserialize(&buffer[..bytes_read])?;
    println!("Result from remote service: {}", response);

    Ok(())
}

use std::io::Error;
use std::net::{TcpListener, TcpStream};
use std::sync::Arc;
use std::thread;
use std::time::Duration;
use serde::{Deserialize, Serialize};
use bincode::{deserialize, serialize};

#[derive(Serialize, Deserialize)]
struct MathService;

// Method to add two numbers
impl MathService {
    fn add(&self, args: (i32, i32)) -> Result<i32, Error> {
        Ok(args.0 + args.1)
    }
}

fn handle_client(mut stream: TcpStream) -> Result<(), Error> {
    let service = MathService;

    loop {
        // Read request from the client
        let mut buffer = [0u8; 1024];
        let bytes_read = stream.read(&mut buffer)?;

        // Deserialize request: (method_name, arguments)
        let request: (String, (i32, i32)) = deserialize(&buffer[..bytes_read])?;

        // Process the request based on the method name
        let result = match request.0.as_str() {
            "add" => service.add(request.1)?,
            _ => 0,
        };

        // Serialize and send the response back to the client
        let response = serialize(&result)?;
        stream.write_all(&response)?;
    }
}

fn main() -> Result<(), Error> {
    // Bind the server to the address and port
    let listener = TcpListener::bind("127.0.0.1:12345")?;
    println!("Server listening on 127.0.0.1:12345");

    // Listen for incoming client connections
    for stream in listener.incoming() {
        let stream = stream?;

        // Spawn a new thread to handle the client's requests
        thread::spawn(move || {
            if let Err(err) = handle_client(stream) {
                println!("Error: {:?}", err);
            }
        });
    }

    Ok(())
}

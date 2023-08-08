import os
import socket

pipe_path = "/tmp/my_named_pipe"

# Connect to the named pipe server
client = socket.socket(socket.AF_UNIX, socket.SOCK_STREAM)
client.connect(pipe_path)

# Send a message to the server
message = "Hello from Python Client!"
client.send(message.encode())

print("Message sent:", message)

# Receive and print the response from the server
response = client.recv(1024).decode()
print("Received response:", response)

client.close()

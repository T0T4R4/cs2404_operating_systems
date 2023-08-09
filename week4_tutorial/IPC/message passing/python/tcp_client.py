import socket

server_ip = 'server_ip_here'
server_port = 12345

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((server_ip, server_port))

message = "Hello from Python Client!"
client_socket.send(message.encode('utf-8'))

client_socket.close()

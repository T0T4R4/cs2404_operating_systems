import socket

server_ip = '0.0.0.0'
server_port = 12345

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind((server_ip, server_port))
server_socket.listen(1)

print("Server listening on {}:{}".format(server_ip, server_port))

client_socket, client_address = server_socket.accept()
print("Connection from:", client_address)

data = client_socket.recv(1024)
print("Received:", data.decode('utf-8'))

client_socket.close()
server_socket.close()

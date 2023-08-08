package main

import (
	"fmt"
	"net"
)

func main() {
	// Replace with the actual IP address of the server
	serverIP := "server_ip_here"
	serverPort := 12345

	// Listen for incoming TCP connections on the specified IP and port
	listener, err := net.Listen("tcp", fmt.Sprintf("%s:%d", serverIP, serverPort))
	if err != nil {
		fmt.Println("Error listening:", err)
		return
	}
	defer listener.Close() // Close the listener when the function exits

	fmt.Printf("Server listening on %s:%d\n", serverIP, serverPort)

	// Accept a new incoming connection
	conn, err := listener.Accept()
	if err != nil {
		fmt.Println("Error accepting connection:", err)
		return
	}
	defer conn.Close() // Close the connection when the function exits

	// Create a buffer to hold received data
	buffer := make([]byte, 1024)

	// Read data from the connection into the buffer
	n, err := conn.Read(buffer)
	if err != nil {
		fmt.Println("Error reading:", err)
		return
	}

	// Print the received data as a string
	fmt.Println("Received:", string(buffer[:n]))
}

package main

import (
	"fmt"
	"net"
)

func main() {
	// Replace with the actual IP address of the server
	serverIP := "server_ip_here"
	serverPort := 12345

	// Establish a TCP connection to the server
	conn, err := net.Dial("tcp", fmt.Sprintf("%s:%d", serverIP, serverPort))
	if err != nil {
		fmt.Println("Error connecting:", err)
		return
	}
	defer conn.Close() // Close the connection when the function exits

	// Message to send to the server
	message := "Hello from Go Client!"

	// Send the message as a byte array over the connection
	_, err = conn.Write([]byte(message))
	if err != nil {
		fmt.Println("Error sending message:", err)
		return
	}
}

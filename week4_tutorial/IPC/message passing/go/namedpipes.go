package main

import (
	"fmt"
	"net"
)

func main() {
	pipePath := "/tmp/my_named_pipe"

	// Connect to the named pipe server
	client, err := net.Dial("unix", pipePath)
	if err != nil {
		fmt.Println("Error connecting:", err)
		return
	}
	defer client.Close()

	// Send a message to the server
	message := "Hello from Go Client!"
	_, err = client.Write([]byte(message))
	if err != nil {
		fmt.Println("Error sending message:", err)
		return
	}

	fmt.Println("Message sent:", message)

	// Receive and print the response from the server
	buffer := make([]byte, 1024)
	n, err := client.Read(buffer)
	if err != nil {
		fmt.Println("Error reading response:", err)
		return
	}

	fmt.Println("Received response:", string(buffer[:n]))
}

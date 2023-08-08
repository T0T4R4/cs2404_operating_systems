package main

import (
	"fmt"
	"net"
	"net/rpc"
)

type MathService struct{} // MathService struct for implementing RPC methods

// Method to add two numbers
func (m *MathService) Add(args *struct{ A, B int }, reply *int) error {
	*reply = args.A + args.B
	return nil
}

func main() {
	mathService := new(MathService)
	rpc.Register(mathService) // Register MathService for RPC

	listener, err := net.Listen("tcp", ":12345")
	if err != nil {
		fmt.Println("Error:", err)
		return
	}
	defer listener.Close()

	fmt.Println("Server running. Press Enter to exit.")
	for {
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error:", err)
			continue
		}
		go rpc.ServeConn(conn) // Serve the RPC connection in a separate goroutine
	}
}

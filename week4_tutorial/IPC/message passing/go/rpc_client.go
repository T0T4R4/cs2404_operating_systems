package main

import (
	"fmt"
	"net/rpc"
)

func main() {
	client, err := rpc.Dial("tcp", "localhost:12345")
	if err != nil {
		fmt.Println("Error:", err)
		return
	}
	defer client.Close()

	args := &struct{ A, B int }{5, 7}
	var result int
	err = client.Call("MathService.Add", args, &result) // Call the remote method
	if err != nil {
		fmt.Println("Error:", err)
		return
	}

	fmt.Println("Result from remote service:", result)
}

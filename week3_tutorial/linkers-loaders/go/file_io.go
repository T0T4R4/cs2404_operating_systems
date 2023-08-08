// file_io.go
package main

import (
	"os"
)

func main() {
	file, err := os.Create("example.txt")
	if err != nil {
		panic(err)
	}
	defer file.Close()

	file.WriteString("Hello, this is a test.")
}

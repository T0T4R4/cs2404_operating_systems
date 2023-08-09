package main

import (
	"fmt"
	"os"
	"syscall"
	"unsafe"
)

func main() {
	// Specify the key and size for shared memory
	key := int64(1234)
	size := int64(1024)

	// Create a shared memory segment
	shmID, _, err := syscall.Syscall(syscall.SYS_SHMGET, key, size, syscall.IPC_CREAT|0666)
	if err != 0 {
		fmt.Printf("Error creating shared memory segment: %v\n", err)
		os.Exit(1)
	}

	// Attach the shared memory segment to a pointer
	shmPtr, _, err := syscall.Syscall(syscall.SYS_SHMAT, shmID, 0, 0)
	if err != 0 {
		fmt.Printf("Error attaching shared memory segment: %v\n", err)
		os.Exit(1)
	}

	// Data to be written to shared memory
	data := []byte("Hello from Go!")

	// Copy the data into shared memory
	copy((*[1 << 30]byte)(unsafe.Pointer(shmPtr))[:len(data)], data)

	fmt.Println("Data written to shared memory")

	// Clean up by detaching the shared memory segment
	_, _, err = syscall.Syscall(syscall.SYS_SHMDT, shmPtr, 0, 0)
	if err != 0 {
		fmt.Printf("Error detaching shared memory segment: %v\n", err)
		os.Exit(1)
	}
}

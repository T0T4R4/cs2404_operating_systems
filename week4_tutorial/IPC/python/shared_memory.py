import mmap

size = 1024
key = 1234

# Create a shared memory segment and map it to a writable memory-mapped file
shm = mmap.mmap(-1, size, mmap.MAP_SHARED, mmap.PROT_WRITE, mmap.MAP_ANONYMOUS | mmap.MAP_SHARED, key)

data_to_write = b"Hello from Python!"
shm.write(data_to_write)

print("Data written to shared memory")

# Read data from the shared memory
shm.seek(0)  # Move the file pointer to the beginning of the shared memory
data_read = shm.read(len(data_to_write)).decode("utf-8")

print(f"Data read from shared memory: {data_read}")

# Clean up by closing the memory-mapped file
shm.close()

"""
 The code above is a Python implementation of a counting semaphore. 
 A counting semaphore is a synchronization primitive that allows a limited number of threads to access a shared resource concurrently. 
 In this implementation, the semaphore is initialized with a limit of 2, 
 meaning that only two threads can access the shared resource at the same time.       
"""

import threading
import time

class Resource:
    def __init__(self):
        self.semaphore = threading.Semaphore(2)  # Limit concurrent access to 2

    def access_resource(self, thread_id):
        print(f"Thread {thread_id} is waiting to access the resource.")
        self.semaphore.acquire()  # Acquire a semaphore slot
        print(f"Thread {thread_id} is now accessing the resource.")
        time.sleep(2)  # Simulate resource usage
        print(f"Thread {thread_id} is done accessing the resource.")
        self.semaphore.release()  # Release the semaphore slot

def main():
    resource = Resource()
    threads = []

    for i in range(5):
        thread = threading.Thread(target=resource.access_resource, args=(i,))
        threads.append(thread)
        thread.start()

    for thread in threads:
        thread.join()

if __name__ == "__main__":
    main()


"""
    Output example

Thread 0 is waiting to access the resource.
Thread 0 is now accessing the resource.
Thread 1 is waiting to access the resource.
Thread 2 is waiting to access the resource.
Thread 1 is now accessing the resource.
    -> From this point onwards, already 2 threads are accessing the resource concurrently.
    -> We have to wait for one of them to finish before another thread can access the resource.

Thread 4 is waiting to access the resource.
Thread 3 is waiting to access the resource.
Thread 1 is done accessing the resource.
    -> Now that Thread 1 is done accessing the resource, any other thread can access the resource.
Thread 0 is done accessing the resource.
Thread 2 is now accessing the resource.
    -> Thread 2 is now able to access the resource.
Thread 2 is done accessing the resource.
Thread 4 is now accessing the resource.
Thread 2 is done accessing the resource.
Thread 4 is done accessing the resource.
Thread 3 is now accessing the resource.
Thread 3 is done accessing the resource.
"""
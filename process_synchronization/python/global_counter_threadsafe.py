"""
    This example illustrates how to use a lock to protect a shared resource.
    The lock is used to synchronize access to the shared resource.
    The lock is acquired before entering the critical section and released after leaving the critical section.
    The lock internally uses a mutex to synchronize access to the shared resource.

    The count variable is a shared resource. Its value is incremented and decremented by multiple threads.
    As threads access the critical section one at a time, the value of count is always 0.
"""

import threading
import time
count = 0
counter_lock = threading.Lock()

def increment_counter():
    global count
    with counter_lock:
        # Start of the critical section
        for _ in range(2):
            count += 1
            time.sleep(.25)
            count -= 1

        print(f"Counter value: {count} (Thread {threading.current_thread().ident})")
        # End of the critical section

# Main thread
threads = [threading.Thread(target=increment_counter) for _ in range(5)]
for thread in threads:
    thread.start()

for thread in threads:
    thread.join()


"""
    Output example: We can see that the value of count is always 0, which is consistent with
    the fact that only one thread can access the critical section at a time.

    Counter value: 0 (Thread 8376)
    Counter value: 0 (Thread 23504)
    Counter value: 0 (Thread 26712)
    Counter value: 0 (Thread 6228)
    Counter value: 0 (Thread 14724)
"""
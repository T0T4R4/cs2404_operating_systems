"""
    This is the non-threadsafe version of the global_counter_threadsafe.py 
    
    This example illustrates how thread concurrency changes the value of a shared resource unexpectedly.
    The count variable is a shared resource. Its value is incremented and decremented by multiple threads.
    As threads access the critical section as they want, the value of count is random.
"""

import threading
import time

count = 0 

def increment_counter():
    # Start of the critical section
    global count
    count += 1
    print(f"Counter value: {count} (Thread {threading.current_thread().ident})")
    time.sleep(.25)
    count -= 1
    # End of the critical section

# Main thread
threads = [threading.Thread(target=increment_counter) for _ in range(5)]
for thread in threads:
    thread.start()

for thread in threads:
    thread.join()

"""
    Output example: we can see that the value of count is random, which is not what we want.
                    It means that the threads are accessing the critical section as they want.

Counter value: 1 (Thread 23052)
Counter value: 2 (Thread 24380)
Counter value: 3 (Thread 15748)
Counter value: 4 (Thread 24132)
Counter value: 5 (Thread 15032)
"""
"""
    This program simulates a database access. The database is accessed by multiple threads.
    The database is a shared resource. The code simulates reading and writing records in the database.
    As threads are accessing the database, the database is locked to prevent other threads from accessing it, 
    which means that only one thread can access the database at a time.
"""

import threading
import time
import random
from colorama import Back, Fore, Style, init

init(autoreset=True) # to reset the colorama styles at every print

db_lock = threading.Lock()

def access_database():
    threadId = f"{threading.current_thread().ident}"
    print(f"Thread {threadId}  started.")
    time.sleep(2)

    with db_lock:
        # Start of the critical section
        print(f"Thread {threadId} is accessing the database.")
        # loop through database records
        for _ in range(3):
            # create a variable which randomly will contain either "reading" or "writing"
            operation = f"{Fore.GREEN}Reading{Style.RESET_ALL}" if random.randint(0, 1) else f"{Fore.RED}Writing{Style.RESET_ALL}"
            print(f"Thread {threadId} is {operation} a record.")
            time.sleep(0.5) 
        print(f"Thread {threadId} has finished with the database.")
        # End of the critical section
        
    print(f"Thread {threadId} has completed ✅.")

# Main thread
threads = []
for _ in range(5):
    thread = threading.Thread(target=access_database)
    thread.start()
    threads.append(thread)

for thread in threads:
    thread.join()

print("Main thread has finished.")

"""
    Output example: We can see that only one thread is accessing the database at a time.

Thread 27048  started.
Thread 12648  started.
Thread 26708  started.Thread 24476  started.
Thread 11588  started.

Thread 11588 is accessing the database.
Thread 11588 is Writing a record.
Thread 11588 is Reading a record.
Thread 11588 is Writing a record.
Thread 11588 has finished with the database.
Thread 11588 has completed ✅.
Thread 12648 is accessing the database.
Thread 12648 is Reading a record.
Thread 12648 is Reading a record.
Thread 12648 is Writing a record.
Thread 12648 has finished with the database.
Thread 12648 has completed ✅.Thread 27048 is accessing the database.

Thread 27048 is Writing a record.
Thread 27048 is Reading a record.
Thread 27048 is Reading a record.
Thread 27048 has finished with the database.
Thread 27048 has completed ✅.
Thread 24476 is accessing the database.
Thread 24476 is Writing a record.
Thread 24476 is Writing a record.
Thread 24476 is Writing a record.
Thread 24476 has finished with the database.
Thread 24476 has completed ✅.Thread 26708 is accessing the database.

Thread 26708 is Writing a record.
Thread 26708 is Writing a record.
Thread 26708 is Writing a record.
Thread 26708 has finished with the database.
Thread 26708 has completed ✅.
Main thread has finished.
"""
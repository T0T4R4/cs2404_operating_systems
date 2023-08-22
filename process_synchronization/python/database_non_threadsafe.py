"""
    This is the non-threadsafe version of the database_threadsafe.py

    This illustrates how thread concurrency can cause unexpected results, such as data corruption.
"""

import threading
import time
import random
from colorama import Back, Fore, Style, init

init(autoreset=True) # to reset the colorama styles at every print

def access_database():
    threadId = f"{threading.current_thread().ident}"
    print(f"Thread {threadId}  started.\n")
    time.sleep(2)
    # Start of the critical section
    print(f"Thread {threadId} is accessing the database.\n")
    # loop through database records
    for _ in range(3):
        # create a variable which randomly will contain either "reading" or "writing"
        operation = f"{Fore.GREEN}Reading{Style.RESET_ALL}" if random.randint(0, 1) else f"{Fore.RED}Writing{Style.RESET_ALL}"
        print(f"Thread {threadId} is {operation} a record.\n")
        time.sleep(0.5) 
    print(f"Thread {threadId} has finished with the database.\n")
    # End of the critical section

    print(f"Thread {threadId} has completed ✅.\n")

# Main thread
threads = []
for _ in range(5):
    thread = threading.Thread(target=access_database)
    thread.start()
    threads.append(thread)

for thread in threads:
    thread.join()

print("Main thread has finished.\n")


"""
    Output example: we can see that all threads are accessing the database at the same time, which is not what we want.

Thread 24704  started.
Thread 13744  started.
Thread 14104  started.
Thread 18504  started.
Thread 19964  started.
Thread 19964 is accessing the database.
Thread 24704 is accessing the database.
Thread 13744 is accessing the database.
Thread 13744 is Writing a record.
Thread 18504 is accessing the database.
Thread 14104 is accessing the database.
Thread 24704 is Reading a record.
Thread 19964 is Reading a record.
Thread 14104 is Reading a record.
Thread 18504 is Writing a record.
Thread 18504 is Reading a record.
Thread 14104 is Reading a record.
Thread 13744 is Reading a record.
Thread 24704 is Writing a record.
Thread 19964 is Writing a record.
Thread 14104 is Reading a record.
Thread 18504 is Reading a record.
Thread 13744 is Reading a record.
Thread 24704 is Writing a record.
Thread 19964 is Reading a record.
Thread 24704 has finished with the database.
Thread 14104 has finished with the database.
Thread 13744 has finished with the database.
Thread 19964 has finished with the database.
Thread 24704 has completed ✅.
Thread 14104 has completed ✅.
Thread 13744 has completed ✅.
Thread 19964 has completed ✅.
Thread 18504 has finished with the database.
Thread 18504 has completed ✅.
Main thread has finished.
"""
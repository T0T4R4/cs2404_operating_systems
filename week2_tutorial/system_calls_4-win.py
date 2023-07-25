# This scripts illustrates the usage of system calls in Python by reading the current system time.
# This is the Windows version of the script.

import win32api
import time

def get_system_time():
    try:
        while True:
            system_time = win32api.GetSystemTime()
            print(f"\rCurrent System Time: {system_time}", end="")
            time.sleep(1)
    except KeyboardInterrupt:
        print("\nExiting...")

if __name__ == "__main__":
    get_system_time()

# This scripts illustrates the usage of system calls in Python by reading the current system time.
# This is the Linux version of the script.

import ctypes
import time

class timeval(ctypes.Structure):
    _fields_ = [
        ('tv_sec', ctypes.c_long),
        ('tv_usec', ctypes.c_long)
    ]

def get_system_time():
    try:
        while True:
            current_time = timeval()
            libc = ctypes.CDLL("libc.so.6")
            libc.gettimeofday(ctypes.byref(current_time), None)

            # Convert the seconds to a human-readable time format
            time_struct = time.localtime(current_time.tv_sec)
            formatted_time = time.strftime("%Y-%m-%d %H:%M:%S", time_struct)

            print(f"\rCurrent System Time: {formatted_time}", end="")
            time.sleep(1)
    except KeyboardInterrupt:
        print("\nExiting...")

if __name__ == "__main__":
    get_system_time()

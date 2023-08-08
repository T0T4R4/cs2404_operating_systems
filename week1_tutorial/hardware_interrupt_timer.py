""" This program demonstrates how to use a timer interrupt to perform a task"""
import threading
import time

def handle_timer_interrupt():
    print("Timer interrupt occurred!")

# Timer function running in a separate thread
def timer_thread():
    time.sleep(5)  # Wait for 5 seconds
    handle_timer_interrupt()

# Create and start the timer thread
timer_thread = threading.Thread(target=timer_thread)
timer_thread.start()

# The main program continues execution
while True:
    # Perform other tasks or operations here
    pass
